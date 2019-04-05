using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depersonalizer.Common
{
	public interface IDataContext
	{
		int StartFrom { get; set; }
		IDataDictionary DataDictionary { get; }
	}
}
