using AutoMapper;
using HrELP.Application.AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.Services.AppUserService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Owin.BuilderProperties;

namespace HrELP.Presentation.Controllers
{//deneme
    [Authorize]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly AddressAPIService _addressAPI;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserService appUserService, IMapper mapper, AddressAPIService addressAPI, IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
            _mapper = mapper;
            _addressAPI = addressAPI;
            _webHostEnvironment = webHostEnvironment;
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
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userWithAddress = await _appUserService.GetUserAsync(user.Id);

            UserDetailVM userDetailVM = new UserDetailVM();
            _mapper.Map(user, userDetailVM);
            userDetailVM.FullAddress = userWithAddress.Address.FullAddress;
            userDetailVM.ZipCode = userWithAddress.Address.PostalCode;

            #region AddressAPI
            var cities = await _addressAPI.GetCitiesAsync();
            userDetailVM.SelectedCity = GetMostSimilarString(userWithAddress.Address.City, cities);

            var towns = await _addressAPI.GetTownsByCityAsync(userDetailVM.SelectedCity);
            userDetailVM.SelectedTown = GetMostSimilarString(userWithAddress.Address.Town, towns);

            var districts = await _addressAPI.GetDistrictsByTown(userDetailVM.SelectedTown, userDetailVM.SelectedCity);
            userDetailVM.SelectedDistrict = GetMostSimilarString(userWithAddress.Address.District, districts);

            var quarters = await _addressAPI.GetQuartersByDistrictAsync(userDetailVM.SelectedTown, userDetailVM.SelectedCity, userDetailVM.SelectedDistrict);
            userDetailVM.SelectedQuarter = GetMostSimilarString(userWithAddress.Address.Quarter, quarters);

            var cityList = cities.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            var townList = towns.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            var districtList = districts.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            var quarterList = quarters.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            #endregion

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
            _mapper.Map(model, update);

            if (model.PhotoFile != null)
            {
                if (model.Photo != null)
                {
                    if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images", "profilepic", user.Photo)))
                    {
                        System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", "profilepic", user.Photo));
                    }
                }
                
                IFormFile formFile = update.PhotoFile;
                var extent = Path.GetExtension(formFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profilepic", randomName);
                update.Photo = randomName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            userWithAddress.Address.City = update.SelectedCity;
            userWithAddress.Address.Town = update.SelectedTown;
            userWithAddress.Address.District = update.SelectedDistrict;
            userWithAddress.Address.Quarter = update.SelectedQuarter;
            userWithAddress.Address.PostalCode = update.ZipCode;
            userWithAddress.Address.FullAddress = update.FullAddress;

            update.UserName = User.Identity.Name;
            await _appUserService.UpdateAsync(update);
            if (user.Photo != null)
            {
                HttpContext.Session.SetString("Photo", user.Photo);
            }
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return View("~/Views/Login/Login.cshtml");
        }
        [HttpGet]
        public async Task<JsonResult> GetTownsForCity(string city)
        {
            var towns = await _addressAPI.GetTownsByCityAsync(city);
            var townList = towns?.Select(town => new SelectListItem { Value = town, Text = town }).ToList();
            return Json(townList);
        }
        [HttpGet]
        public async Task<JsonResult> GetDistrictsForTown(string city, string town)
        {
            var districts = await _addressAPI.GetDistrictsByTown(town, city);
            var districtList = districts.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            return Json(districtList);
        }
        [HttpGet]
        public async Task<JsonResult> GetQuartersForDistrictAsync(string city, string town, string district)
        {
            var quarters = await _addressAPI.GetQuartersByDistrictAsync(town, city, district);
            var quarterList = quarters.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            return Json(quarterList);
        }
        [HttpGet]
        public static string GetMostSimilarString(string target, List<string> stringList)
        {
            int minDistance = int.MaxValue;
            string mostSimilarString = null;

            foreach (string str in stringList)
            {
                int distance = ComputeLevenshteinDistance(target, str);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    mostSimilarString = str;
                }
            }

            return mostSimilarString;
        }
        private static int ComputeLevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= t.Length; j++)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= t.Length; j++)
            {
                for (int i = 1; i <= s.Length; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[s.Length, t.Length];
        }
    }
}
