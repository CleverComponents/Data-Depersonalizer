using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class DataContext : IDataContext
	{
		public DataContext()
		{
			DataDictionary = new DataDictionary();
		}

		public int StartFrom { get; set; }

		public IDataDictionary DataDictionary { get; }
	}
}
