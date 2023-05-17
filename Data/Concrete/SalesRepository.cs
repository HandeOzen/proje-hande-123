using Data.Abstract;
using Data.Contexts;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class SalesRepository:GenericRepository<Sales>,ISalesRepository
    {
        public SalesRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        
        
        {
        
        
        }
        private ApplicationDbContext ApplicationDbContext
        {
            get
            {

                return context as ApplicationDbContext;
            }
        }

        public int GetCountOfTodaySales()
        {
         return   ApplicationDbContext.Sales.Select(x=>x).Where(x=>x.CreatedOn.Day==DateTimeOffset.Now.Day).ToList().Count;
        }

       

        public double GetTotalPrice(DateTimeOffset date)
        {
           return ApplicationDbContext.Sales.Select(x => x).Where(x => x.CreatedOn.Day == DateTimeOffset.Now.Day).Sum(x=>Convert.ToDouble(x.Price));
        }
    }
}
