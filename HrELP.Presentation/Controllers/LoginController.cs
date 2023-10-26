using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AppUserService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrELP.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public LoginController(IAppUserService appUserService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _appUserService.LoginAsync(loginDTO);
                    if (result.Succeeded)
                    {
                        var user = await _appUserService.GetUserWithEmail(loginDTO.UserName);
                        var role = await _userManager.GetRolesAsync(user);
                        HttpContext.Session.SetString("UserName", user.FirstName);
                        HttpContext.Session.SetString("UserRole", role[0]);
                        if(user.Photo != null) 
                        {
                            HttpContext.Session.SetString("Photo", user.Photo);
                        }
                        UserDetailBaseVM  udb = new UserDetailBaseVM();
                        _mapper.Map(user, udb);
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password."); // Add an error message
					TempData["ErrorMessage"] = "Invalid username or password.";
                    return View(loginDTO);
                }       
            }
            return View(loginDTO);
        }
		[HttpGet]
		public async Task<IActionResult> CreatePassword(string token, string userId)
		{
			if (token == null || userId == null)
			{
				ModelState.AddModelError("", "Invalid password create token.");
			}

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreatePassword(CreatePasswordVM vm)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(vm.userId);
				if (user != null)
				{
					if (vm.Password == vm.ReTypePassword)
					{
						user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, vm.Password);
						if (user.PasswordHash != null)
						{
							TempData["ErrorMessage"] = "Your Password has been successfully created. You can now login to the system.";
                            await _userManager.UpdateAsync(user);
							return RedirectToAction("Login", "Login");
						}
					}
					else
					{
						ModelState.AddModelError("", "Passwords don't match.");
						return View(vm);
					}
				}
				TempData["ErrorMessage"] = "User not found.";
				return View(vm);
			}
			return View(vm);
		}
	}
}
