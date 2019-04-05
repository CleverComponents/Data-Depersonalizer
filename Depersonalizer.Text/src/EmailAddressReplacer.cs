using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class EmailAddressReplacer : DataReplacer
	{
		private string[] ExtractEmails(string text)
		{
			const string matchPattern =
				@"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
				@"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
				@"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
				@"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

			return ExtractData(text, matchPattern, RegexOptions.IgnoreCase);
		}

		private string GetEncodedEmail(string email)
		{
			var bytes = Encoding.Default.GetBytes(email);
			var base64 = Convert.ToBase64String(bytes);
			return Uri.EscapeDataString(base64);
		}

		private string ReplaceEmails(string source, IDataContext context)
		{
			var emails = ExtractEmails(source);

			foreach (var email in emails)
			{
				var depersonalizedEmail = context.DataDictionary.GetValue(email, () => { return String.Format(ReplaceMask, context.StartFrom++); });

				source = source.Replace(email, depersonalizedEmail);

				var encodedEmail = GetEncodedEmail(email);
				source = source.Replace(encodedEmail, "");
			}

			return source;
		}

		public EmailAddressReplacer(IDataReplacer nextReplacer) : base(nextReplacer) { }

		public EmailAddressReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceEmails(source, context);
			return base.Replace(source, context);
		}

		public string ReplaceMask { get; set; }
	}
}
