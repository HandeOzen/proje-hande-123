using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ISalesRepository:IRepository<Sales>
    {
        int GetCountOfTodaySales();
        double GetTotalPrice(DateTimeOffset date);
    }
}
