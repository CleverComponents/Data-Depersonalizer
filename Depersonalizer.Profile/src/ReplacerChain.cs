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

using System.Collections.Generic;
using Depersonalizer.Common;

namespace Depersonalizer.Profile
{
	public class ReplacerChain
	{
		public string Replace(string source, IDataContext context)
		{
			return Root?.Replace(source, context) ?? source;
		}

		public void MoveUp(IDataReplacer replacer)
		{
			if (replacer == null) return;

			var prev2 = GetPrevious(replacer);
			var prev1 = GetPrevious(prev2);
			var next = replacer.NextReplacer;

			if (prev2 != null)
			{
				prev2.NextReplacer = next;
				replacer.NextReplacer = prev2;

				if (prev1 != null)
				{
					prev1.NextReplacer = replacer;
				}

				if (Root == prev2)
				{
					Root = replacer;
				}
			}
		}

		public void MoveDown(IDataReplacer replacer)
		{
			if (replacer == null) return;

			var next1 = replacer.NextReplacer;
			var next2 = next1?.NextReplacer ?? null;
			var prev = GetPrevious(replacer);

			replacer.NextReplacer = next2;

			if (next1 != null)
			{
				next1.NextReplacer = replacer;

				if (Root == replacer)
				{
					Root = next1;
				}

				if (prev != null)
				{
					prev.NextReplacer = next1;
				}
			}
		}

		public void Remove(IDataReplacer replacer)
		{
			if (replacer == null) return;

			var next = replacer.NextReplacer;
			replacer.NextReplacer = null;

			var prev = GetPrevious(replacer);
			if (prev != null)
			{
				prev.NextReplacer = next;
			}
			else
			{
				Root = next;
			}
		}

		public void Add(IDataReplacer replacer)
		{
			if (replacer == null) return;

			var last = Last;
			if (last != null)
			{
				last.NextReplacer = replacer;
			}
			else
			{
				Root = replacer;
			}
		}

		public IDataReplacer GetPrevious(IDataReplacer replacer)
		{
			var current = Root;
			if (current == null || replacer == null) return null;

			while (current != null)
			{
				if (current.NextReplacer == replacer)
				{
					return current;
				}

				current = current.NextReplacer;
			}
			return null;
		}

		public IDataReplacer GetNext(IDataReplacer replacer)
		{
			return replacer?.NextReplacer ?? null;
		}

		public List<IDataReplacer> ToList()
		{
			var list = new List<IDataReplacer>();

			var replacer = Root;

			while (replacer != null)
			{
				list.Add(replacer);
				replacer = replacer.NextReplacer;
			}

			return list;
		}

		public IDataReplacer Root { get; set; }

		public IDataReplacer Last
		{
			get
			{
				var replacer = Root;
				if (replacer == null) return null;

				while (replacer.NextReplacer != null)
				{
					replacer = replacer.NextReplacer;
				}
				return replacer;
			}
		}
	}
}
