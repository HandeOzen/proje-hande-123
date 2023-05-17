using Business.Abstract;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TanksManager:ITanksService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TanksManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Tanks entity)
        {
            _unitOfWork.Tanks.Create(entity);
            _unitOfWork.Save();
        }

        public List<Tanks> GetAll()
        {
          return _unitOfWork.Tanks.GetAll();
        }

        public Tanks GetById(int id)
        {
            return _unitOfWork.Tanks.GetById(id);
        }

        public FuelTypes GetFuelTypeByTankId(int tankId)
        {
            return _unitOfWork.Tanks.GetFuelTypeByTankId(tankId);
        }

        public void Update(Tanks tanks)
        {
            _unitOfWork.Tanks.Update(tanks);
            _unitOfWork.Save();
        }
    }
}
