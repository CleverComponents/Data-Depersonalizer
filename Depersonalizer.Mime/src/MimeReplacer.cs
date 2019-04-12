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
	public class MimeReplacer : IDataReplacer
	{
		private string ReplaceHeader(string source, IDataContext context)
		{
			return HeaderReplacer?.Replace(source, context) ?? source;
		}

		private string ReplaceText(string source, IDataContext context)
		{
			return TextBodyReplacer?.Replace(source, context) ?? source;
		}

		private string ReplaceHtml(string source, IDataContext context)
		{
			return HtmlBodyReplacer?.Replace(source, context) ?? source;
		}

		private string ReplaceTextAttachment(string source, IDataContext context)
		{
			return TextAttachmentReplacer?.Replace(source, context) ?? source;
		}

		private void ReplaceTextBody(TextBody body, IDataContext context)
		{
			if (body != null)
			{
				var source = string.Join("\r\n", body.Strings);
				source = ReplaceText(source, context);
				body.Strings = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			}
		}

		private void ReplaceHtmlBody(TextBody body, IDataContext context)
		{
			if (body != null)
			{
				var source = string.Join("\r\n", body.Strings);
				source = ReplaceHtml(source, context);
				body.Strings = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			}
		}

		private Stream ReplaceAttachmentBody(Stream source, AttachmentBody attachment, IDataContext context)
		{
			var destination = new MemoryStream();

			source.Position = 0;

			if (attachment.ContentType.IndexOf("text/") == 0)
			{
				var encoding = Translator.GetEncoding(GetAttachmentCharSet(attachment));

				string content;

				using (var reader = new StreamReader(source, encoding, false, 8192, true))
				{
					content = reader.ReadToEnd();
				}

				content = ReplaceTextAttachment(content, context);

				using (var writer = new StreamWriter(destination, encoding, 8192, true))
				{
					writer.Write(content);
				}
			}
			else
			{
				source.CopyTo(destination);
			}

			destination.Position = 0;

			return destination;
		}

		private string GetAttachmentCharSet(AttachmentBody attachment)
		{
			var headerFieldList = new HeaderFieldList();

			headerFieldList.Parse(0, attachment.RawHeader);

			var result = headerFieldList.GetFieldValue("Content-Type");

			result = headerFieldList.GetFieldValueItem(result, "charset");

			return result;
		}

		public MimeReplacer(IDataReplacer headerReplacer, IDataReplacer textBodyReplacer, IDataReplacer htmlBodyReplacer, IDataReplacer textAttachmentReplacer)
		{
			HeaderReplacer = headerReplacer;
			TextBodyReplacer = textBodyReplacer;
			HtmlBodyReplacer = htmlBodyReplacer;
			TextAttachmentReplacer = textAttachmentReplacer;
		}

		public string Replace(string source, IDataContext context)
		{
			using (var mailMessage = new MailMessage())
			{
				var attachments = new Dictionary<AttachmentBody, Stream>();

				mailMessage.SaveAttachment += (object sender, GetBodyStreamEventArgs e) =>
				{
					e.Stream = new MemoryStream();
					e.Handled = true;
				};

				mailMessage.AttachmentSaved += (object sender, AttachmentSavedEventArgs e) =>
				{
					attachments.Add(e.Body, ReplaceAttachmentBody(e.Data, e.Body, context));
				};

				mailMessage.LoadAttachment += (object sender, GetBodyStreamEventArgs e) =>
				{
					e.Stream = attachments[e.Body];
					e.Handled = true;
				};

				source = ReplaceHeader(source, context);

				mailMessage.MessageSource = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);

				ReplaceTextBody(mailMessage.Text, context);
				ReplaceHtmlBody(mailMessage.Html, context);

				return string.Join("\r\n", mailMessage.MessageSource);
			}
		}

		public IDataReplacer HeaderReplacer { get; set; }
		public IDataReplacer TextBodyReplacer { get; set; }
		public IDataReplacer HtmlBodyReplacer { get; set; }
		public IDataReplacer TextAttachmentReplacer { get; set; }
	}
}
