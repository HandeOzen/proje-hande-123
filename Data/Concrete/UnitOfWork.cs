using Data.Abstract;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            
        }
        private  TanksRepository _tanksRepository;
        private  SalesRepository _salesRepository;
        private  FuelTypesRepository _fuelTypesRepository;
            public ITanksRepository Tanks => _tanksRepository=_tanksRepository ?? new TanksRepository(_context);

        public ISalesRepository Sales => _salesRepository=_salesRepository?? new SalesRepository(_context);

        public IFuelTypesRepository FuelTypes =>_fuelTypesRepository=_fuelTypesRepository?? new FuelTypesRepository(_context);
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
;        }
    }
}
