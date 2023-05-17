using Business.Abstract;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FuelTypesManager:IFuelTypesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FuelTypesManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<FuelTypes> GetAll()
        {
         return   _unitOfWork.FuelTypes.GetAll();
        }

        public FuelTypes GetById(int id)
        {
            return _unitOfWork.FuelTypes.GetById(id);
        }

        public double GetPriceById(int id)
        {
           return _unitOfWork.FuelTypes.GetPriceById(id);
        }

        public void Update(FuelTypes entity)
        {
            _unitOfWork.FuelTypes.Update(entity);
            _unitOfWork.Save();
        }
    }
}
