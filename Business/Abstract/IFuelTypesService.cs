using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFuelTypesService
    {
        List<FuelTypes> GetAll();
         double GetPriceById(int id);
        void Update(FuelTypes entity);
        FuelTypes GetById(int id);
    }
}
