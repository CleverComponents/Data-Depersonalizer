using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class IpAddressReplacer : DataReplacer
	{
		private string[] ExtractIpAddresses(string text)
		{
			const string matchPattern = @"([0-9]{1,3}[\.]){3}[0-9]{1,3}";

			return ExtractData(text, matchPattern, RegexOptions.IgnoreCase);
		}

		private int NormalizeIpPart(int part)
		{
			if (part > -1 && part < 256)
			{
				return part;
			}
			return part % 100;
		}

		private string ReplaceIpAddresses(string source, IDataContext context)
		{
			var ipAddresses = ExtractIpAddresses(source);

			foreach (var ip in ipAddresses)
			{
				var depersonalizedIp = context.DataDictionary.GetValue(ip, () => { return String.Format(ReplaceIpAddr, NormalizeIpPart(context.StartFrom++)); });
				source = source.Replace(ip, depersonalizedIp);
			}

			return source;
		}

		public IpAddressReplacer(IDataReplacer nextReplacer) : base(nextReplacer) { }

		public IpAddressReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceIpAddresses(source, context);
			return base.Replace(source, context);
		}

		public string ReplaceIpAddr { get; set; }
	}
}
