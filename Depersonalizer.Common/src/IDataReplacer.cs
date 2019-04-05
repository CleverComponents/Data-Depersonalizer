using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depersonalizer.Common
{
	public interface IDataReplacer
	{
		string Replace(string source, IDataContext context);
		IDataReplacer NextReplacer { get; }
	}
}
