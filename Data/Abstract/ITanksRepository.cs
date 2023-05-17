using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ITanksRepository:IRepository<Tanks>
    {
        FuelTypes GetFuelTypeByTankId(int tankId);
      //  void UpdateQuantity(int tankId ,double quantity);
    }
}
