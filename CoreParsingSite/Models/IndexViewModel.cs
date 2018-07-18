using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreParsingSite.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Goods> _Goods { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
