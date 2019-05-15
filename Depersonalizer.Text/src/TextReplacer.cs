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
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class TextReplacer : DataReplacer
	{
		private string ReplaceByDictionary(string source, IDataDictionary dictionary)
		{
			foreach (var item in dictionary)
			{
				source = ReplaceValue(source, item.Key, item.Value);
			}
			return source;
		}

		protected virtual string ReplaceValue(string source, string key, string value)
		{
			return source.Replace(key, value);
		}

		public TextReplacer() : base()
		{
			ReplaceKeys = new List<ReplaceParameter>();
		}

		public override string Replace(string source, IDataContext context)
		{
			if (ReplaceKeys.Count == 0)
			{
				source = ReplaceByDictionary(source, context.DataDictionary);
			}
			else
			{
				var dictionary = new DataDictionary();

				foreach (var key in ReplaceKeys)
				{
					if (!string.IsNullOrEmpty(key.Parameter))
					{
						dictionary.AddValue(key.Parameter, key.ReplaceWith);
					}
				}

				source = ReplaceByDictionary(source, dictionary);
			}

			return base.Replace(source, context);
		}

		public List<ReplaceParameter> ReplaceKeys { get; }
	}
}
