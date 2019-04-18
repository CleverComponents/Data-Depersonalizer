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

		public MimeReplacer()
		{
			MimePartReplacers = new List<IMimePartReplacer>();
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

				return string.Join("\r\n", mailMessage.MessageSource);
			}
			finally
			{
				attachments = null;
				mailMessage.Dispose();
				mailMessage = null;
			}
		}

		public IMimePartReplacer AddMimePartReplacer(IMimePartReplacer replacer)
		{
			MimePartReplacers.Add(replacer);
			replacer.MimeReplacer = this;
			return replacer;
		}

		public List<IMimePartReplacer> MimePartReplacers { get; }
	}
}
