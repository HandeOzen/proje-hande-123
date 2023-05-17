using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITanksService
    {
        List<Tanks> GetAll();
        FuelTypes GetFuelTypeByTankId(int tankId);
        Tanks GetById(int id);
        void Update(Tanks tanks);
    }
}
