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
	public class XmlDocumentReplacer : DataReplacer
	{
		private string ReplaceXmlNode(string nodeName, string replaceWithMask, string source, IDataContext context)
		{
			if (string.IsNullOrEmpty(nodeName)) return source;

			string matchPattern = @"<" + nodeName + ">(.*?)</" + nodeName + ">";

			var xmlNodes = ExtractGroupData(source, matchPattern, 1, RegexOptions.IgnoreCase);

			foreach (var node in xmlNodes)
			{
				var depersonalized = context.DataDictionary.GetValue(node.GroupValue, () => { return String.Format(replaceWithMask, context.StartFrom++); });
				var depersonalizedTag = node.DataValue.Replace(node.GroupValue.Trim(), depersonalized);
				source = source.Replace(node.DataValue, depersonalizedTag);
			}

			return source;
		}

		private string ReplaceXmlNodes(string source, IDataContext context)
		{
			foreach (var node in XmlReplaceNodes)
			{
				source = ReplaceXmlNode(node.Parameter, node.ReplaceWith, source, context);
			}

			return source;
		}

		public XmlDocumentReplacer() : base()
		{
			XmlReplaceNodes = new List<ReplaceParameter>();
		}

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceXmlNodes(source, context);
			return base.Replace(source, context);
		}

		public List<ReplaceParameter> XmlReplaceNodes { get; }
	}
}
