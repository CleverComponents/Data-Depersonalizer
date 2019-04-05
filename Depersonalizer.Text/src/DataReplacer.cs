using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public abstract class DataReplacer : IDataReplacer
	{
		public DataReplacer(IDataReplacer nextReplacer)
		{
			NextReplacer = nextReplacer;
		}

		public DataReplacer()
		{
			NextReplacer = null;
		}

		public virtual string Replace(string source, IDataContext context)
		{
			return NextReplacer?.Replace(source, context) ?? source;
		}

		public string[] ExtractData(string text, string matchPattern, RegexOptions regexOptions)
		{
			var regex = new Regex(matchPattern, regexOptions);

			var matches = regex.Matches(text);

			var list = new List<string>();

			foreach (Match match in matches)
			{
				var data = match.Value;

				if (list.IndexOf(data) < 0)
				{
					list.Add(data);
				}
			}

			return list.ToArray();
		}

		public IDataReplacer NextReplacer { get; }
	}
}
