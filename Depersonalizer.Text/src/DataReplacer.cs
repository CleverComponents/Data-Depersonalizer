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
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public abstract class DataReplacer : IDataReplacer
	{
		public class GroupDataInfo
		{
			public string DataValue { get; set; }
			public string GroupValue { get; set; }
		}

		protected DataReplacer() { }

		public virtual string Replace(string source, IDataContext context)
		{
			return NextReplacer?.Replace(source, context) ?? source;
		}

		public string[] ExtractSimpleData(string source, string matchPattern, RegexOptions regexOptions)
		{
			var regex = new Regex(matchPattern, regexOptions);

			var matches = regex.Matches(source);

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

		public List<GroupDataInfo> ExtractGroupData(string source, string matchPattern, int groupIndex, RegexOptions regexOptions)
		{
			var regex = new Regex(matchPattern, regexOptions);

			var matches = regex.Matches(source);

			var list = new List<GroupDataInfo>();

			foreach (Match match in matches)
			{
				var data = match.Value;

				if (!list.Any(x => x.DataValue.Equals(match.Value, StringComparison.OrdinalIgnoreCase)))
				{
					var group = match.Groups[groupIndex];
					list.Add(new GroupDataInfo() { DataValue = match.Value, GroupValue = group.Value });
				}
			}

			return list;
		}

		public IDataReplacer NextReplacer { get; set; }
	}
}
