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
using System.Text;

namespace Depersonalizer.Text
{
	public static class HtmlEncoder
	{
		private static uint[] StringToArrayOfUtf32Chars(string source)
		{
			Byte[] bytes = Encoding.UTF32.GetBytes(source);
			uint[] utf32Chars = (uint[])Array.CreateInstance(typeof(uint), bytes.Length / sizeof(uint));

			for (int i = 0, j = 0; i < bytes.Length; i += 4, ++j)
			{
				utf32Chars[j] = BitConverter.ToUInt32(bytes, i);
			}

			return utf32Chars;
		}

		public static string EncodeEntities(string source, bool hexFormat = false)
		{
			uint[] utf32Chars = StringToArrayOfUtf32Chars(source);
			StringBuilder sb = new StringBuilder(2000);

			foreach (uint codePoint in utf32Chars)
			{
				if (codePoint > 0x0000007F)
				{
					sb.AppendFormat(hexFormat ? "&#x{0:X};" : "&#{0};", codePoint);
				}
				else
				{
					char ch = Convert.ToChar(codePoint);

					switch (ch)
					{
						case '"': sb.Append("&quot;"); break;
						case '\'': sb.Append("&apos;"); break;
						case '&': sb.Append("&amp;"); break;
						case '<': sb.Append("&lt;"); break;
						case '>': sb.Append("&gt;"); break;
						default: sb.Append(ch.ToString()); break;
					}
				}
			}

			return sb.ToString();
		}

		public static string DecodeEntities(string source)
		{
			return System.Web.HttpUtility.HtmlDecode(source);
		}
	}
}
