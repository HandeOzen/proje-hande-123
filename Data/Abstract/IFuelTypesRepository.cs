using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IFuelTypesRepository:IRepository<FuelTypes>
    {
        double GetPriceById(int id); 
    }
}
