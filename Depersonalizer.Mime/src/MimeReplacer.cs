using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public MimeReplacer(IDataReplacer nextReplacer)
		{
			NextReplacer = nextReplacer;
		}

		public string Replace(string source, IDataContext context)
		{
			using (var mailMessage = new MailMessage())
			{
				source = ReplaceData(source, context);

				mailMessage.MessageSource = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);

				ReplaceContent(mailMessage.Text, context);
				ReplaceContent(mailMessage.Html, context);

				//TODO handle attachments

				return string.Join("\r\n", mailMessage.MessageSource);
			}
		}

		public IDataReplacer NextReplacer { get; }
	}
}
