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
	public class IpAddressReplacer : DataReplacer
	{
		private string[] ExtractIpAddresses(string text)
		{
			const string matchPattern = @"([0-9]{1,3}[\.]){3}[0-9]{1,3}";

			return ExtractSimpleData(text, matchPattern, RegexOptions.IgnoreCase);
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

		public IpAddressReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			source = ReplaceIpAddresses(source, context);
			return base.Replace(source, context);
		}

		public string ReplaceIpAddr { get; set; }
	}
}
