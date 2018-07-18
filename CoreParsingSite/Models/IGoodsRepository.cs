using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreParsingSite.Models
{
    public interface IGoodsRepository
    {
        void Create(Goods goods);
        void Delete(int id);
        Goods Get(int id);
        List<Goods> GetGoods();
        void Update(Goods goods);
        
    }
}
