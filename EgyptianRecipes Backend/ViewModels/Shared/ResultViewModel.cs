using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ResultViewModel<T>
    {
        public bool Successed { get; set; } = false;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
