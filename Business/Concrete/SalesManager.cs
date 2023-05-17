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
    public class SalesManager:ISalesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalesManager(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }

        public void Create(Sales entity)
        {
            _unitOfWork.Sales.Create(entity);
            _unitOfWork.Save();
        }

        public List<Sales> GetAll()
        {
         return _unitOfWork.Sales.GetAll();
        }

        public int GetCountOfTodaySales()
        {
            return _unitOfWork.Sales.GetCountOfTodaySales();
        }

        public double GetTotalPrice(DateTimeOffset date)
        {
           return _unitOfWork.Sales.GetTotalPrice(date);
        }
    }
}
