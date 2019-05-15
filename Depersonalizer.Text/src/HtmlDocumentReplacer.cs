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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class HtmlDocumentReplacer : DataReplacer
	{
		private string ReplaceTag(string id, string replaceWithMask, string source, IDataContext context)
		{
			if (string.IsNullOrEmpty(id)) return source;

			string matchPattern = "<(\\w+)([^>]*id[\\s]?=[\\s]?['\"]" + id + "['\"][\\s\\S]*?)>([\\s\\S]*?)<\\/\\1>";

			var tags = ExtractGroupData(source, matchPattern, 3, RegexOptions.IgnoreCase);

			foreach (var tag in tags)
			{
				var decodedTagValue = HtmlEncoder.DecodeEntities(tag.GroupValue);

				var depersonalized = context.DataDictionary.GetValue(decodedTagValue, () =>	{ return String.Format(replaceWithMask, context.StartFrom++); });

				depersonalized = HtmlEncoder.EncodeEntities(depersonalized);

				var depersonalizedTag = tag.DataValue.Replace(tag.GroupValue.Trim(), depersonalized);

				source = source.Replace(tag.DataValue, depersonalizedTag);
			}

			return source;
		}

		private string ReplaceTags(string source, IDataContext context)
		{
			foreach (var tag in ReplaceTagIds)
			{
				source = ReplaceTag(tag.Parameter, tag.ReplaceWith, source, context);
			}

			return source;
		}

		public HtmlDocumentReplacer() : base()
		{
			ReplaceTagIds = new List<ReplaceParameter>();
		}

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceTags(source, context);
			return base.Replace(source, context);
		}

		public List<ReplaceParameter> ReplaceTagIds { get; }
	}
}
