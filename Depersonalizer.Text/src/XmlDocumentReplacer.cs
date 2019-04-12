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
	public class XmlDocumentReplacer : DataReplacer
	{
		private string[] ExtractXmlNodes(string nodeName, string text)
		{
			string matchPattern = @"<" + nodeName + ">(.*?)</" + nodeName + ">";

			return ExtractData(text, matchPattern, RegexOptions.IgnoreCase);
		}

		private string ReplaceXmlNode(string nodeName, string replaceWithMask, string source, IDataContext context)
		{
			var xmlNodes = ExtractXmlNodes(nodeName, source);

			foreach (var node in xmlNodes)
			{
				var depersonalizedNode = context.DataDictionary.GetValue(node, () =>
				{
					var replaceWith = String.Format(replaceWithMask, context.StartFrom++);
					return String.Format("<{0}>{1}</{0}>", nodeName, replaceWith);
				});
				source = source.Replace(node, depersonalizedNode);
			}

			return source;
		}

		private string ReplaceXmlNodes(string source, IDataContext context)
		{
			if (XmlNodes == null || XmlReplaceWith == null)
			{
				return source;
			}

			if (XmlNodes.Length != XmlReplaceWith.Length)
			{
				throw new Exception("The number of XML nodes must be the same as Replace With values");
			}

			for (int i = 0; i < XmlNodes.Length; i++)
			{
				source = ReplaceXmlNode(XmlNodes[i], XmlReplaceWith[i], source, context);
			}

			return source;
		}

		public XmlDocumentReplacer(IDataReplacer nextReplacer) : base(nextReplacer) { }

		public XmlDocumentReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceXmlNodes(source, context);
			return base.Replace(source, context);
		}

		public string[] XmlNodes { get; set; }
		public string[] XmlReplaceWith { get; set; }
	}
}
