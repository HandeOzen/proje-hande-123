using Data.Abstract;
using Entity;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class FuelTypesRepository:GenericRepository<FuelTypes>,IFuelTypesRepository
    {
        public FuelTypesRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) { }

        public double GetPriceById(int id)
        {
          return  context.Set<FuelTypes>().Find(id).Price;
        }
    }
}
