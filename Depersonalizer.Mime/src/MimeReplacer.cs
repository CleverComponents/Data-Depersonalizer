using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CleverComponents.InetSuite;
using Depersonalizer.Common;

namespace Depersonalizer.Mime
{
	public class MimeReplacer : IDataReplacer
	{
		private string ReplaceData(string source, IDataContext context)
		{
			return NextReplacer?.Replace(source, context) ?? source;
		}

		private void ReplaceContent(TextBody textBody, IDataContext context)
		{
			if (textBody != null)
			{
				var source = string.Join("\r\n", textBody.Strings);
				source = ReplaceData(source, context);
				textBody.Strings = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			}
		}

		private Stream ReplaceAttachment(Stream source, AttachmentBody attachment, IDataContext context)
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

				content = ReplaceData(content, context);

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

		public MimeReplacer(IDataReplacer nextReplacer)
		{
			NextReplacer = nextReplacer;
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
					attachments.Add(e.Body, ReplaceAttachment(e.Data, e.Body, context));
				};

				mailMessage.LoadAttachment += (object sender, GetBodyStreamEventArgs e) =>
				{
					e.Stream = attachments[e.Body];
					e.Handled = true;
				};

				source = ReplaceData(source, context);

				mailMessage.MessageSource = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);

				ReplaceContent(mailMessage.Text, context);
				ReplaceContent(mailMessage.Html, context);

				return string.Join("\r\n", mailMessage.MessageSource);
			}
		}

		public IDataReplacer NextReplacer { get; }
	}
}
