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
	public class HtmlReplacer : DataReplacer
	{
		private string ReplaceTag(string id, string replaceWithMask, string source, IDataContext context)
		{
			string matchPattern = "<(\\w+)([^>]*id[\\s]?=[\\s]?['\"]" + id + "['\"][\\s\\S]*?)>([\\s\\S]*?)<\\/\\1>";

			var tags = ExtractGroupData(source, matchPattern, 3, RegexOptions.IgnoreCase);

			foreach (var tag in tags)
			{
				var depersonalized = context.DataDictionary.GetValue(tag.GroupValue, () =>	{ return String.Format(replaceWithMask, context.StartFrom++); });
				var depersonalizedTag = tag.DataValue.Replace(tag.GroupValue.Trim(), depersonalized);
				source = source.Replace(tag.DataValue, depersonalizedTag);
			}

			return source;
		}

		private string ReplaceTags(string source, IDataContext context)
		{
			if (TagIds == null || TagReplaceWith == null)
			{
				return source;
			}

			if (TagIds.Length != TagReplaceWith.Length)
			{
				throw new Exception("The number of HTML tags must be the same as Replace With values");
			}

			for (int i = 0; i < TagIds.Length; i++)
			{
				source = ReplaceTag(TagIds[i], TagReplaceWith[i], source, context);
			}

			return source;
		}

		public HtmlReplacer(IDataReplacer nextReplacer) : base(nextReplacer) { }

		public HtmlReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceTags(source, context);
			//TODO replace text by dictionary

			return base.Replace(source, context);
		}

		public string[] TagIds { get; set; }
		public string[] TagReplaceWith { get; set; }
	}
}
