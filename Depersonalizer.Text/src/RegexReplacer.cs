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
#endregion

using System;
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
