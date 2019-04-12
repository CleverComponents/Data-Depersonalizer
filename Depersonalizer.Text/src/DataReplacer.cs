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

using System.Collections.Generic;
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
