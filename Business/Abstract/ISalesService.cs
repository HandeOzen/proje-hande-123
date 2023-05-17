using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISalesService
    {
        void Create(Sales entity);
        List<Sales> GetAll();
        int GetCountOfTodaySales();
        double GetTotalPrice(DateTimeOffset date);
    }
}
