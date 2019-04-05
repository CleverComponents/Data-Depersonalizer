using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class RegexReplacer : DataReplacer
	{
		private string ReplaceCustomPattern(string matchPattern, string replaceWithMask, string source, IDataContext context)
		{
			var dataArray = ExtractData(source, matchPattern, RegexOptions.IgnoreCase);

			foreach (var data in dataArray)
			{
				var depersonalizedData = context.DataDictionary.GetValue(data, () => { return String.Format(replaceWithMask, context.StartFrom++); });
				source = source.Replace(data, depersonalizedData);
			}

			return source;
		}

		private string ReplaceCustomPatterns(string source, IDataContext context)
		{
			if (RegexPatterns == null || RegexReplaceWith == null)
			{
				return source;
			}

			if (RegexPatterns.Length != RegexReplaceWith.Length)
			{
				throw new Exception("The number of Regex patterns must be the same as Replace With values");
			}

			for (int i = 0; i < RegexPatterns.Length; i++)
			{
				source = ReplaceCustomPattern(RegexPatterns[i], RegexReplaceWith[i], source, context);
			}

			return source;
		}

		public RegexReplacer(IDataReplacer nextReplacer) : base(nextReplacer) { }

		public RegexReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceCustomPatterns(source, context);
			return base.Replace(source, context);
		}

		public string[] RegexPatterns { get; set; }
		public string[] RegexReplaceWith { get; set; }
	}
}
