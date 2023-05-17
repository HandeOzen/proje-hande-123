using Business.Abstract;
using Data.Contexts;
using Entity;
using FuelAutomation.Entity;
using FuelAutomation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace FuelAutomation.Controllers
{
    
   [Authorize(Policy ="RequireAdmin")]
  //  [Route("Admin/")]
    public class AdminController : Controller
    {

        private ISalesService _salesService;
        private UserManager<Users> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ITanksService _tanksService;
        private IFuelTypesService _fuelTypesService;
        private ApplicationDbContext _context;
        private IPasswordHasher<Users> _passwordHasher;
        readonly SignInManager<Users> _signInManager;
        public AdminController(ISalesService salesService, UserManager<Users> userManager, ITanksService tanksService, IFuelTypesService fuelTypesService, 
            ApplicationDbContext context,RoleManager<IdentityRole> roleManager,IPasswordHasher<Users> passwordHasher, SignInManager<Users> signInManager )
        {
           
            _userManager = userManager;
            _fuelTypesService = fuelTypesService;
            _tanksService = tanksService;
            _salesService = salesService;
            _context = context;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }
       
        public IActionResult Dashboard()
        {
            ViewBag.FuelTypes = _fuelTypesService.GetAll();
            ViewBag.Tanks = _tanksService.GetAll();
            ViewBag.CountDailySales=_salesService.GetCountOfTodaySales
                ();
            ViewBag.CountOfUser = _userManager.Users.Count();
            ViewBag.TotalPrice=_salesService.GetTotalPrice(DateTimeOffset.Now);
            return View();
        }
       // [Route("Tanks")]
        public IActionResult Tanks()
        {
            List<Tanks> tanks=_tanksService.GetAll();
            
            List<TanksViewModel> tanksList  = new List<TanksViewModel>();

            for(int i = 0; i < tanks.Count; i++)
            {
               // var t = _tanksService.GetFuelTypeByTankId(Convert.ToInt32(tanks[i].Id));
                tanksList.Add(new TanksViewModel()
                {

                    Capacity = tanks[i].Capacity,
                    Quantity = tanks[i].Quantity,
                    FuelType = _fuelTypesService.GetById(tanks[i].FuelTypesId).Name,
                    id = tanks[i].Id

                }) ;



            }
            return View(tanksList);
        }
        [Route("Admin/TankDischarge/{id?}")]
        public IActionResult TankDischarge(int? id)

        {
            TankDischargeViewModel tankDischargeViewModel = new TankDischargeViewModel();
            var tank = _tanksService.GetById(Convert.ToInt32(id));
            if (tank == null)
            {


                return NotFound("Böyle Bir Tank Bulunmamaktadır");
            }
            else
            {
                tankDischargeViewModel.Capacity = tank.Capacity;
               tankDischargeViewModel.FuelType = _tanksService.GetFuelTypeByTankId(Convert.ToInt32(id)).Name;
                tankDischargeViewModel.Quantity = tank.Quantity;
                tankDischargeViewModel.Id = Convert.ToInt32(id);

            }


            return View(tankDischargeViewModel);
           
        }
        [HttpPost]
        [Route("Admin/TankDischarge/{id?}")]
        public IActionResult TankDischarge(TankDischargeViewModel tankDischargeViewModel)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "Lütfen bilgileri doğru bir şekilde doldurunuz");
                return View(tankDischargeViewModel);

            }
            if (Convert.ToDouble(tankDischargeViewModel.DischargeQuantity) > tankDischargeViewModel.Quantity)
            {


                ModelState.AddModelError("DischargeQuantity", "Boşaltım Miktarı Mevcut Miktardan Fazla Olamaz!!");
                return View(tankDischargeViewModel);

            }
            else
            {

                Tanks newTank = _tanksService.GetById(Convert.ToInt32(tankDischargeViewModel.Id));
                // newTank.Id = Convert.ToInt32(tankFillViewModel.Id);
                newTank.Quantity = Convert.ToDouble(tankDischargeViewModel.Quantity - Convert.ToDouble(tankDischargeViewModel.DischargeQuantity));

              //  _tanksService.Update(newTank);


                try
                {


                    _tanksService.Update(newTank);
                    TempData["Success"] = " Tank boşaltma tamamlandı.";
                    return RedirectToAction();

                }
                catch (Exception ex)
                {
                    TempData["Fail"] = "Tank boşaltma başarısız.";

                }
            }

            return RedirectToAction();
        }
        [Route("Admin/TankFill/{id?}")]
        public IActionResult TankFill(int? id)
        {

            TankFillViewModel tankFillViewModel = new TankFillViewModel();
            var tank = _tanksService.GetById(Convert.ToInt32(id));
            if (tank == null)
            {


                return NotFound("Böyle Bir Tank Bulunmamaktadır");
            }
            else
            {
                tankFillViewModel.Capacity = tank.Capacity;
                tankFillViewModel.FuelType = _tanksService.GetFuelTypeByTankId(Convert.ToInt32(id)).Name;
                tankFillViewModel.Quantity= tank.Quantity;
                tankFillViewModel.Id= Convert.ToInt32(id);

            }


            return View(tankFillViewModel);
        }
        [HttpPost]
        [Route("Admin/TankFill/{id?}")]
        public IActionResult TankFill(TankFillViewModel tankFillViewModel)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "Lütfen bilgileri doğru bir şekilde doldurunuz");
                return View( tankFillViewModel);

            }
            if(Convert.ToDouble(tankFillViewModel.FillQuantity)>tankFillViewModel.Capacity-tankFillViewModel.Quantity)
            {


                ModelState.AddModelError("FillQuantity", "Dolum Miktarı Boş Kapasiteden Fazla Olamaz!!");
                return View(tankFillViewModel);

            }
            else
            {

                Tanks newTank = _tanksService.GetById(Convert.ToInt32(tankFillViewModel.Id));
               // newTank.Id = Convert.ToInt32(tankFillViewModel.Id);
                newTank.Quantity =Convert.ToDouble( tankFillViewModel.Quantity) + Convert.ToDouble(tankFillViewModel.FillQuantity);
                try
                {


                    _tanksService.Update(newTank);
                    TempData["Success"] = " Tanka dolum tamamlandı.";
                    return RedirectToAction();

                }
                catch (Exception ex)
                {
                    TempData["Fail"] = "Tanka dolum yapılamadı.";

                }


            }
           
            return RedirectToAction();
        }
        public IActionResult UserOperations()
        {
            var result = _context.Users
             .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
             .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
             .Select(c => new UsersViewModel()
             {
                 FirstName = c.ur.u.FirstName,
                 LastName = c.ur.u.LastName,
                 Email = c.ur.u.Email,
                 Role = c.r.Name,
                 UserId=c.ur.u.Id
             }).ToList().GroupBy(uv => new { uv.FirstName, uv.Email,uv.LastName, uv.UserId }).Select(r => new UsersViewModel()
             {
                 FirstName = r.Key.FirstName,
                 LastName = r.Key.LastName,
                 Email = r.Key.Email,
                 UserId=r.Key.UserId,

                 Role = string.Join(",", r.Select(c => c.Role).ToArray())
             }).ToList();

            ViewBag.Users = result;
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> UserAdd(UserAddViewModel userAddViewModel )
        {
            if (!ModelState.IsValid)
            {



                return View("UserOperations");
            }
            var user = new Users()
            {

                FirstName= userAddViewModel.FirstName,
                LastName= userAddViewModel.LastName,
                Email= userAddViewModel.Email,
                UserName= userAddViewModel.Email


            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, userAddViewModel.Password);
            if (identityResult.Succeeded)
            {

                var resultRole = await _userManager.AddToRoleAsync(user, userAddViewModel.Role);
                if (resultRole.Succeeded)
                {
                    TempData["Success"] = "Kullanıcı  başarıyla oluşturuldu";
                    return RedirectToAction("UserOperations");


                }
                else
                {
                    TempData["Fail"] = "Kullanıcı  oluşturulamadı";
                    return RedirectToAction("UserOperations");
                }

            }
           

                 else
                {
                    TempData["Fail"] = "Kullanıcı  oluşturulamadı";
                    return RedirectToAction("UserOperations");
                }
            //return RedirectToAction("UserOperations");
        }

        public async Task<IActionResult> UserEdit(string id)
        {
           UserUpdateViewModel userUpdateViewModel=new UserUpdateViewModel();   
            var user = await _userManager.FindByIdAsync(id);
            var oldRole=await _userManager.GetRolesAsync(user);
            string role = oldRole.FirstOrDefault();
            userUpdateViewModel.Role = role;
            userUpdateViewModel.FirstName = user.FirstName;
            userUpdateViewModel.LastName = user.LastName;
            userUpdateViewModel.Email = user.Email;
            userUpdateViewModel.UserId= user.Id;
           // ViewBag.User = user;
            
            return View(userUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserUpdateViewModel userUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(userUpdateViewModel);

            }
            else
            {

                var user = await _userManager.FindByIdAsync(userUpdateViewModel.UserId);

                user.FirstName= userUpdateViewModel.FirstName;
                user.LastName= userUpdateViewModel.LastName;
                user.Email= userUpdateViewModel.Email;
                user.PasswordHash = _passwordHasher.HashPassword(user, userUpdateViewModel.Password);
                var oldRole = await _userManager.GetRolesAsync(user);
                IdentityResult removeResult= await _userManager.RemoveFromRoleAsync(user, oldRole.FirstOrDefault());
                IdentityResult addroleResult=  await _userManager.AddToRoleAsync(user, userUpdateViewModel.Role);
                IdentityResult updateResult = await _userManager.UpdateAsync(user);
                if (removeResult.Succeeded && addroleResult.Succeeded && updateResult.Succeeded)
                {
                    var userUpdated = await _userManager.FindByIdAsync(userUpdateViewModel.UserId);
                    userUpdateViewModel.FirstName = user.FirstName;
                    userUpdateViewModel.LastName  = user.LastName;
                    userUpdateViewModel.Email  = user.Email;
                    userUpdateViewModel.UserId = user.Id;
                    TempData["Success"] = "Kullanıcı bilgileri başarıyla güncellendi";
                    return View(userUpdateViewModel);

                }
                else
                {

                    TempData["Fail"] = "Kullanıcı bilgileri güncellenemedi";
                    return View(userUpdateViewModel);

                }



                
            }
           



            
            return View();
        }
        public async Task<  IActionResult> SaleHistory()
        {
            List<Sales> sales = new List<Sales>();
           sales = _salesService.GetAll();
         //   var fuelType = _tanksService.GetFuelTypeByTankId(sales[0].TanksId);
            List<SaleHistoryViewModel> history = new List<SaleHistoryViewModel>();

            for(int i = 0; i < sales.Count; i++)
            {
                
               var user = await _userManager.FindByIdAsync(sales[i].UserId);
               string userName = user.FirstName + " " + user.LastName;
               
                history.Add(new SaleHistoryViewModel()
                {

                    CarPlate = sales[i].CarPlate,
                    Name = userName,
                    Price = sales[i].Price,
                    Quantity = sales[i].Quantity,
                    FuelType = _tanksService.GetFuelTypeByTankId(sales[i].TanksId).Name,
                    CreatedOn = sales[i].CreatedOn,
                   
                }); ;






            }
          return   View(history);
        }
        public IActionResult UpdatePrice()
        {
            ViewBag.FuelTypes = _fuelTypesService.GetAll();
          //  ViewBag.Tanks = _tanksService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePrice(List< UpdatePriceViewModel>updatePriceViewModel)
        {
            ViewBag.FuelTypes = _fuelTypesService.GetAll();
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "Lütfen Geçerli Değerler Giriniz");
                return View();
            }
            var fuelTypes=_fuelTypesService.GetAll();
            int i = 0;
            foreach(var fueltype in fuelTypes)
            {
                foreach(var model in updatePriceViewModel)
                {

                    if (model.Name == fueltype.Name)
                    {

                        try
                        {

                            fueltype.Price = Convert.ToDouble(updatePriceViewModel[i].Price);
                            _fuelTypesService.Update(fueltype);
                           
                        }
                        catch
                        {

                            TempData["Fail"] = "Fiyat güncelleme başarısız oldu.";

                        }
                       

                    }


                }

              
               

            }
            TempData["Success"] = " Fiyatlar başarıyla güncellendi";
            return RedirectToAction();
            //return View();
        }


        [HttpPost]
        [Route("Admin/UserDelete/{id?}")]
        public async Task<IActionResult> UserDelete(string? id)
        {

            Users user=await _userManager.FindByIdAsync(id);
            if(user != null)
            {

                var roles= _userManager.GetRolesAsync(user);

                if(roles != null)
                {

                  // foreach(var role in roles.Result.ToList()) {
                    
                    
                    var result=await _userManager.RemoveFromRolesAsync(user,roles.Result);
                    
                    
                   // }
                }
                IdentityResult deleteResult= await _userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    TempData["SuccessDelete"] = "Kullanıcı başarıyla silindi";
                   
                   // return RedirectToAction("UserOperations");


                }
                else
                {

                    TempData["FailDelete"] = "Kullanıcı silinemedi";

                }
            }
            return RedirectToAction("UserOperations");

        }
    }
}
