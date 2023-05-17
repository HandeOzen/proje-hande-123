using Data.Abstract;
using Data.Contexts;
using Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class TanksRepository : GenericRepository<Tanks>,ITanksRepository
    {
        public TanksRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        
        {
        
        }
        private ApplicationDbContext ApplicationDbContext 
        { get {

                return context as ApplicationDbContext;
           }  }

        public FuelTypes GetFuelTypeByTankId(int tankId)
        {
            return ApplicationDbContext.Set<Tanks>().Select(x => x).Where(x => x.Id == tankId).Select(x=>x.FuelTypes).FirstOrDefault();
         
        }

        //public void UpdateQuantity(int tankId,double quantity)
        
        //{
        //    Tanks tanks = new Tanks();
        //   tanks = context.Set<Tanks>().Select(x => x.Id == tankId).First();
        //    tank.Quantity=
        //}
           
       
    }
}
