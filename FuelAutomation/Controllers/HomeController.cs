using Business.Abstract;
using Entity;
using FuelAutomation.Entity;
using FuelAutomation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace FuelAutomation.Controllers
{
  [Authorize(Policy ="RequireStaff")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IFuelTypesService _fuelTypesService;
        private ISalesService _salesService;
        private ITanksService _tanksService;
        private UserManager<Users> _userManager;
        public HomeController(ILogger<HomeController> logger,IFuelTypesService fuelTypesService, ISalesService salesService,ITanksService tanksService,UserManager<Users> userManager)
        {
            _logger = logger;
            _fuelTypesService = fuelTypesService;
            _salesService = salesService;
            _tanksService=tanksService
                ;
            _userManager=userManager;

        }

        public IActionResult Index()
        {
          ViewBag.FuelTypes=  _fuelTypesService.GetAll();
            ViewBag.Tanks=_tanksService.GetAll();
            return View();
        }



        [HttpPost]
     // [Route("Home/Index")]
        public IActionResult Index(CreateSaleModel createSaleModel)
        {
            ViewBag.FuelTypes = _fuelTypesService.GetAll();
            ViewBag.Tanks = _tanksService.GetAll();
            if (!ModelState.IsValid)
            {

                return View();

            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            Sales sale= new Sales();
           // var fuelType = _tanksService.GetFuelTypeByTankId(createSaleModel.TankId);
            var priceFuel = _tanksService.GetById(createSaleModel.TankId).FuelTypes.Price;
           // var priceFuel = _fuelTypesService.GetPriceById(fuelType.Id);
            sale.CarPlate = createSaleModel.CarPlate;
            sale.Price = (Convert.ToInt32(createSaleModel.Quantity) * priceFuel).ToString();
            sale.CreatedOn = DateTimeOffset.UtcNow;
            sale.UserId = _userManager.GetUserId(currentUser);
            sale.Quantity=createSaleModel.Quantity;
            sale.TanksId = createSaleModel.TankId;

            var tank = _tanksService.GetById(createSaleModel.TankId);
            if (Convert.ToDouble(createSaleModel.Quantity )> tank.Quantity)
            {
                ModelState.AddModelError("Quantity", "Satılacak miktar tankta olan miktardan fazla olamaz");
                return View(createSaleModel);
                //return RedirectToAction();

            }

            try
            {

                tank.Quantity = tank.Quantity - Convert.ToDouble(createSaleModel.Quantity);
                _tanksService.Update(tank);
                _salesService.Create(sale);
                TempData["Success"] = " Satış başarılı";
                return RedirectToAction();
            }
            catch
            {

                TempData["Fail"] = "Satış işlemi başarısız.";

            }
           
            return RedirectToAction("Index");
            
        }
    }
}