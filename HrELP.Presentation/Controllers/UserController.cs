using AutoMapper;
using HrELP.Application.AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.Services.AppUserService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Owin.BuilderProperties;

namespace HrELP.Presentation.Controllers
{//deneme
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly AddressAPIService _addressAPI;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserService appUserService, IMapper mapper, AddressAPIService addressAPI)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
            _mapper = mapper;
            _addressAPI = addressAPI;
        }
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> Index()
        {   
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userWithAddress = await _appUserService.GetUserAsync(user.Id);
            UserDetailBaseVM userDetailBaseVM = new UserDetailBaseVM();
            _mapper.Map(user, userDetailBaseVM);
            user.Address = userWithAddress.Address;
            return View(userDetailBaseVM);
        }
        public async Task<IActionResult> Details()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userWithAddress = await _appUserService.GetUserAsync(user.Id);
            UserDetailVM userDetailsVM = new UserDetailVM();
            _mapper.Map(user, userDetailsVM);
            userDetailsVM.FullAddress = userWithAddress.Address.FullAddress;
            return View(userDetailsVM);
        }
        public async Task<IActionResult> Edit()
        {
            var cities = _addressAPI.GetCitiesAsync();
            var cityList = cities.Select(x => new SelectListItem { Value = x, Text = x }).ToList();

            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userWithAddress = await _appUserService.GetUserAsync(user.Id);

            UserDetailVM userDetailVM = new UserDetailVM();
            _mapper.Map(user, userDetailVM);
            userDetailVM.FullAddress = userWithAddress.Address.FullAddress;
            userDetailVM.ZipCode = userWithAddress.Address.PostalCode;
            userDetailVM.SelectedCity = userWithAddress.Address.City;
            userDetailVM.SelectedTown = userWithAddress.Address.Town;
            userDetailVM.SelectedDistrict = userWithAddress.Address.District;
            userDetailVM.SelectedQuarter = userWithAddress.Address.Quarter;
            var towns = _addressAPI.GetTownsByCityAsync(userWithAddress.Address.City);
            var districts = _addressAPI.GetDistrictsByTown(userWithAddress.Address.Town, userWithAddress.Address.City);
            var quarters = _addressAPI.GetQuartersByDistrictAsync(userWithAddress.Address.Town, userWithAddress.Address.City, userWithAddress.Address.District);
            var townList = towns.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            var districtList = districts.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            var quarterList = quarters.Select(x => new SelectListItem { Value = x, Text = x }).ToList();

            ViewBag.City = cityList;
            ViewBag.Town = townList;
            ViewBag.District = districtList;
            ViewBag.Quarter = quarterList;

            return View(userDetailVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDetailVM model)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userWithAddress = await _appUserService.GetUserAsync(user.Id);
            UpdateUserDTO update = new UpdateUserDTO();
            _mapper.Map(model,update);
            userWithAddress.Address.City = update.SelectedCity;
            userWithAddress.Address.Town = update.SelectedTown;
            userWithAddress.Address.District = update.SelectedDistrict;
            userWithAddress.Address.Quarter = update.SelectedQuarter;
            userWithAddress.Address.PostalCode = update.ZipCode;
            userWithAddress.Address.FullAddress = update.FullAddress;

            update.UserName = User.Identity.Name;
            await _appUserService.UpdateAsync(update);
            return RedirectToAction("Details");
        }

		public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public JsonResult GetTownsForCity(string city)
        {
            var towns = _addressAPI.GetTownsByCityAsync(city);
            var townList = towns?.Select(town => new SelectListItem { Value = town, Text = town }).ToList();
            return Json(townList);
        }
        [HttpGet]
        public JsonResult GetDistrictsForTown(string city, string town)
        {
            var districts = _addressAPI.GetDistrictsByTown(town, city);
            var districtList = districts.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            return Json(districtList);
        }
        [HttpGet]
        public JsonResult GetQuartersForDistrict(string city, string town, string district)
        {
            var quarters = _addressAPI.GetQuartersByDistrictAsync(town, city, district);
            var quarterList = quarters.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            return Json(quarterList);
        }
    }
}
