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

//The current version of Data Depersonalizer needs for the non-free library
//Clever Internet.NET Suite. This is a drawback, and we suggest the task of
//changing the program so that it does the same job without the non-free library.
//Anyone who thinks of doing substantial further work on the program,
//first may free it from dependence on the non-free library.
#endregion

using System;
using System.IO;
using Depersonalizer.Common;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Xml;

namespace Depersonalizer.Profile
{
    public class DepersonalizerProfile
    {
		public void Clear()
		{
			ReplacerChain = null;
		}

		public void Load(string fileName)
		{
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				var serializer = new ConfigurationContainer().Create();
				ReplacerChain = serializer.Deserialize<IDataReplacer>(stream);
			}
		}

		public void Save(string fileName)
		{
			if (ReplacerChain == null)
			{
				throw new Exception("The replacers are not defined, there is nothing to save.");
			}

			using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
			{
				var serializer = new ConfigurationContainer().Create();
				serializer.Serialize(stream, ReplacerChain);
			}
		}

		public IDataReplacer ReplacerChain { get; set; }
	}
}
