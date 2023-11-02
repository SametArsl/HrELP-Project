using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AppUserService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace HrELP.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _IWebHostEnvironment;

        public LoginController(IAppUserService appUserService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IWebHostEnvironment IWebHostEnvironment)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _IWebHostEnvironment = IWebHostEnvironment;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            HttpContext.Session.SetString("Photo", "");
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
        public async Task<IActionResult> CreatePassword(string token,string UserId)
        {
           
            if (token == null || UserId==null )
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
                AppUser user = await _appUserService.GetUserWithEmailAsync(vm);
               
                if (user != null)
                {
                    if (vm.Password == vm.ReTypePassword)
                    {
                        user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, vm.Password);
                        if (user.PasswordHash != null)
                        {
                            TempData["ErrorMessage"] = "Your Password has been successfully created. You can now login to the system.";
                            user.EmailConfirmed = true;
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

         [Route("{Controller}/{Action}")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [Route("{Controller}/{Action}")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM vm)
        {
           AppUser user = await _appUserService.GetUserWithEmailAsync(vm);

            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var tokenResult = await _userManager.SetAuthenticationTokenAsync(user, "ResetPassword", "PasswordReset", passwordToken);
                var resetLink = Url.Action("CreatePassword", "Login",
                    new { UserId = user.Id, token = passwordToken }, Request.Scheme
                );

                string HtmlBody = "";
                var PathToFile = Path.Combine(_IWebHostEnvironment.WebRootPath, "EmailTemplate", "index.html");

                var builder = new BodyBuilder();
                using (StreamReader sr = System.IO.File.OpenText(PathToFile))
                {
                    builder.HtmlBody = sr.ReadToEnd();
                }

                builder.HtmlBody = builder.HtmlBody.Replace("{0}", user.FullName);
                builder.HtmlBody = builder.HtmlBody.Replace("{1}", resetLink);
                builder.HtmlBody = builder.HtmlBody.Replace("{2}", resetLink);

                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress("noreplyhrelp@gmail.com");
                mail.Subject = "Forget Password";
                mail.Body = builder.HtmlBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                    smtp.EnableSsl = true; 
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("noreplyhrelp@gmail.com", "rmzqiazgoktnbcac");

                    try
                    {
                        smtp.Send(mail);
                        return RedirectToAction("Login", "Login");
                    }
                    catch (Exception ex)
                    {                        
                        throw new Exception(ex.Message.ToString());
                    }
                }               
            }

            return View();
        }
       public IActionResult Error()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                
                return View("Error");
            }
            else if (statusCode == 403)
            {
                
                return View("Error403");
            }
            
            return View("Error");
        }

	}
}
