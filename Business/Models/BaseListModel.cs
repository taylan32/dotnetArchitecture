using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
	public class BaseListModel<T> : BasePageableModel
	{
		public IList<T>? Items { get; set; }
	}
}
