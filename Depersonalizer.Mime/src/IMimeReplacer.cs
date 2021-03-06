﻿#region license
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

//The current version of Data Depersonalizer needs for the non-free library
//Clever Internet.NET Suite. This is a drawback, and we suggest the task of
//changing the program so that it does the same job without the non-free library.
//Anyone who thinks of doing substantial further work on the program,
//first may free it from dependence on the non-free library.
#endregion

using System.Collections.Generic;
using Depersonalizer.Common;

namespace Depersonalizer.Mime
{
	public interface IMimeReplacer : IDataReplacer
	{
		void ReplaceHeader(IMimePartReplacer replacer, IDataContext context);
		void ReplaceText(IMimePartReplacer replacer, IDataContext context);
		void ReplaceHtml(IMimePartReplacer replacer, IDataContext context);
		void ReplaceTextAttachment(IMimePartReplacer replacer, IDataContext context);

		IList<IMimePartReplacer> MimePartReplacers { get; }
	}
}
