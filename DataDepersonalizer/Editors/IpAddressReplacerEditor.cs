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
	public class IpAddressReplacerEditor : ReplacerEditor
	{
		private TextBox txtIpAddrReplaceMask;

		private void BindControls()
		{
			txtIpAddrReplaceMask.TextChanged += TxtIpAddrReplaceMask_TextChanged;
		}

		private void TxtIpAddrReplaceMask_TextChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void LoadData()
		{
			txtIpAddrReplaceMask.Text = Data.ReplaceIpAddr;
		}

		protected override void SaveData()
		{
			Data.ReplaceIpAddr = txtIpAddrReplaceMask.Text;
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;
			txtIpAddrReplaceMask.ReadOnly = disabled;
		}

		public IpAddressReplacerEditor(TextBox txtIpAddrReplaceMask) : base()
		{
			this.txtIpAddrReplaceMask = txtIpAddrReplaceMask;

			BindControls();
		}

		public new IpAddressReplacer Data
		{
			get { return (IpAddressReplacer)base.Data; }
		}
	}
}
