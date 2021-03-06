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
#endregion

using System.Collections.Generic;
using System.Windows.Forms;
using Depersonalizer.Text;

namespace DataDepersonalizer.Editors
{
	public class TextReplacerEditor : ListReplaceEditor
	{
		protected override List<ReplaceParameter> GetParameters()
		{
			return Data.ReplaceKeys;
		}

		public TextReplacerEditor(DataGridView gridView) : base(gridView) { }

		public new TextReplacer Data
		{
			get { return (TextReplacer)base.Data; }
		}
	}
}
