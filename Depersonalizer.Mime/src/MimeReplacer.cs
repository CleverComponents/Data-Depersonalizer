#region license
//Copyright(C) 2019 by Clever Components

//Author: Sergey Shirokov<admin@clevercomponents.com>
//Website: www.CleverComponents.com

//This file is part of the Data Depersonalizer application.
//The Data Depersonalizer application is free software:
//you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License version 3
//as published by the Free Software Foundation and appearing in the
//included file COPYING.LESSER.

//The Data Depersonalizer application is distributed in the hope
//that it will be useful, but WITHOUT ANY WARRANTY; without even the
//implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License
//along with the Data Depersonalizer application. If not, see<http://www.gnu.org/licenses/>.

//The current version of Data Depersonalizer needs for the non-free library
//Clever Internet.NET Suite. This is a drawback, and we suggest the task of
//changing the program so that it does the same job without the non-free library.
//Anyone who thinks of doing substantial further work on the program,
//first may free it from dependence on the non-free library.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using CleverComponents.InetSuite;
using Depersonalizer.Common;

namespace Depersonalizer.Mime
{
	public class MimeReplacer : IMimeReplacer
	{
		private class Attachments : Dictionary<AttachmentBody, Stream> { }

		private MailMessage mailMessage;
		private Attachments attachments;

		private string GetAttachmentCharSet(AttachmentBody attachment)
		{
			var headerFieldList = new HeaderFieldList();

			headerFieldList.Parse(0, attachment.RawHeader);

			var result = headerFieldList.GetFieldValue("Content-Type");

			result = headerFieldList.GetFieldValueItem(result, "charset");

			return result;
		}

		private string[] ReplaceStrings(string[] source, IMimePartReplacer replacer, IDataContext context)
		{
			if (source == null) return null;

			var s = string.Join("\r\n", source);
			s = replacer.Replace(s, context);
			return s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
		}

		private void ReplacersChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (var item in e.NewItems)
				{
					((IMimePartReplacer)item).MimeReplacer = this;
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (var item in e.OldItems)
				{
					((IMimePartReplacer)item).MimeReplacer = null;
				}
			}
		}

		public MimeReplacer()
		{
			var collection = new ObservableCollection<IMimePartReplacer>();
			MimePartReplacers = collection;
			collection.CollectionChanged += ReplacersChanged;
		}

		public void ReplaceHeader(IMimePartReplacer replacer, IDataContext context)
		{
			mailMessage.From.FullAddress = replacer.Replace(mailMessage.From.FullAddress, context);
			mailMessage.ToList.EmailAddresses = replacer.Replace(mailMessage.ToList.EmailAddresses, context);
			mailMessage.CcList.EmailAddresses = replacer.Replace(mailMessage.CcList.EmailAddresses, context);
			mailMessage.BccList.EmailAddresses = replacer.Replace(mailMessage.BccList.EmailAddresses, context);
			mailMessage.Subject = replacer.Replace(mailMessage.Subject, context);
			mailMessage.ReplyTo = replacer.Replace(mailMessage.ReplyTo, context);
			mailMessage.ReadReceiptTo = replacer.Replace(mailMessage.ReadReceiptTo, context);
			mailMessage.NewsGroups = ReplaceStrings(mailMessage.NewsGroups, replacer, context);
			mailMessage.References = ReplaceStrings(mailMessage.References, replacer, context);
			mailMessage.ExtraFields = ReplaceStrings(mailMessage.ExtraFields, replacer, context);
		}

		public void ReplaceText(IMimePartReplacer replacer, IDataContext context)
		{
			if (mailMessage.Text != null)
			{
				mailMessage.Text.Strings = ReplaceStrings(mailMessage.Text.Strings, replacer, context);
			}
		}

		public void ReplaceHtml(IMimePartReplacer replacer, IDataContext context)
		{
			if (mailMessage.Html != null)
			{
				mailMessage.Html.Strings = ReplaceStrings(mailMessage.Html.Strings, replacer, context);
			}
		}

		public void ReplaceTextAttachment(IMimePartReplacer replacer, IDataContext context)
		{
			foreach (var attachment in attachments)
			{
				if (attachment.Key.ContentType.IndexOf("text/") == 0)
				{
					var encoding = Translator.GetEncoding(GetAttachmentCharSet(attachment.Key));

					string content;

					using (var reader = new StreamReader(attachment.Value, encoding, false, 8192, true))
					{
						content = reader.ReadToEnd();
					}

					content = replacer.Replace(content, context);

					attachment.Value.SetLength(0);

					using (var writer = new StreamWriter(attachment.Value, encoding, 8192, true))
					{
						writer.Write(content);
					}

					attachment.Value.Position = 0;
				}
			}
		}

		public string Replace(string source, IDataContext context)
		{
			mailMessage = mailMessage = new MailMessage();
			try
			{
				attachments = new Attachments();

				mailMessage.SaveAttachment += (object sender, GetBodyStreamEventArgs e) =>
				{
					e.Stream = new MemoryStream();
					e.Handled = true;
				};

				mailMessage.AttachmentSaved += (object sender, AttachmentSavedEventArgs e) =>
				{
					var stream = new MemoryStream();
					attachments.Add(e.Body, stream);

					e.Data.Position = 0;
					e.Data.CopyTo(stream, 8192);
					stream.Position = 0;
				};

				mailMessage.LoadAttachment += (object sender, GetBodyStreamEventArgs e) =>
				{
					e.Stream = attachments[e.Body];
					e.Handled = true;
				};

				mailMessage.MessageSource = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);

				foreach (var replacer in MimePartReplacers)
				{
					replacer.ReplacePart(context);
				}

				//TODO remove DKIM-Signature: DomainKey-Signature:
				//TODO put on top Received: Return-Path:

				return string.Join("\r\n", mailMessage.MessageSource);
			}
			finally
			{
				attachments = null;
				mailMessage.Dispose();
				mailMessage = null;
			}
		}

		public IList<IMimePartReplacer> MimePartReplacers { get; }

		public IDataReplacer NextReplacer
		{
			get { return null; }
			set { System.Diagnostics.Debug.Assert(false, "Not supported."); }
		}
	}
}
