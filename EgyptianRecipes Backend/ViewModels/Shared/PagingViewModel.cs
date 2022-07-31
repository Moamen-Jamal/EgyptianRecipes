using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PagingViewModel
    {
        public int PageSize { set; get; }
        public int PageIndex { set; get; }
        public int Records { set; get; }
        public int Pages { set; get; }
        public IEnumerable<object> Result { set; get; }
    }
}
