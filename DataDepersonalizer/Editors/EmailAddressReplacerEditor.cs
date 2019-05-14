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
using System.Windows.Forms;
using Depersonalizer.Text;

namespace DataDepersonalizer.Editors
{
	public class EmailAddressReplacerEditor : ReplacerEditor
	{
		private TextBox txtEmailReplaceMask;

		private void BindControls()
		{
			txtEmailReplaceMask.TextChanged += TxtEmailReplaceMask_TextChanged;
		}

		private void TxtEmailReplaceMask_TextChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void LoadData()
		{
			txtEmailReplaceMask.Text = Data.ReplaceMask;
		}

		protected override void SaveData()
		{
			Data.ReplaceMask = txtEmailReplaceMask.Text;
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;
			txtEmailReplaceMask.ReadOnly = disabled;
		}

		public EmailAddressReplacerEditor(TextBox txtIpAddrReplaceMask) : base()
		{
			this.txtEmailReplaceMask = txtIpAddrReplaceMask;

			BindControls();
		}

		public new EmailAddressReplacer Data
		{
			get { return (EmailAddressReplacer)base.Data; }
		}
	}
}
