using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        ITanksRepository Tanks { get; }
        ISalesRepository Sales { get; }
        IFuelTypesRepository FuelTypes { get; }
        void Save();
    }
}
