using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.Services.AddressService;
using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.EmailService;
using HrELP.Application.Services.ExpenseRequestService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Infrastructure;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.BuilderProperties;
using Address = HrELP.Domain.Entities.Concrete.Address;


namespace HrELP.Presentation.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AddressAPIService _addressAPI;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IExpenseRequestService _expenseRequestService;
        public ManagerController(SignInManager<AppUser> signInManager, AddressAPIService addressAPI, IAppUserService appUserService, ICompanyService companyService, IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService, IExpenseRequestService expenseRequestService)
        {
            _signInManager = signInManager;
            _addressAPI = addressAPI;
            _appUserService = appUserService;
            _companyService = companyService;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _expenseRequestService = expenseRequestService;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        [Route("{Controller}/{Action}")]
        [HttpGet]
        public IActionResult AddPersonnel()
        {
            List<string> departments = new List<string>
            {
                "Software Development",
                "Testing and Quality Assurance",
                "Product Management",
                "Customer Support",
                "Sales and Marketing",
                "Human Resources",
                "Finance and Accounting",
                "Management"
            };
            List<string> positions = new List<string>
            {
                "Software Developer",
                "QA Engineer / Tester",
                "Project Manager",
                "Business Analyst",
                "Data Scientist",
                "Database Administrator",
                "System Administrator",
                "UX/UI Designer",
                "DevOps Engineer",
                "Security Specialist",
                "Support Engineer",
                "Systems Analyst",
                "Mobile App Developer",
                "Machine Learning Engineer"
            };

            // Her departmana karşılık gelen pozisyonları oluştur


            // ViewBag'e departmanları ve pozisyonları ekle
            ViewBag.Departments = departments;
            ViewBag.Positions = positions;
            var cities = _addressAPI.GetCitiesAsync();
            var cityList = cities.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
            ViewBag.City = cityList;
            AddPersonnelDTO dTO = new AddPersonnelDTO();
            dTO.BirthDate = DateTime.Now.AddYears(-50);
            return View(dTO);
        }
        [Route("{Controller}/{Action}")]
        [HttpPost]
        public async Task<IActionResult> AddPersonnel(AddPersonnelDTO dTO)
        {
            AppUser appUser = await _signInManager.UserManager.GetUserAsync(User);
            Address address = new Address()
            {
                City = dTO.SelectedCity,
                Town = dTO.SelectedTown,
                District = dTO.SelectedDistrict,
                Quarter = dTO.SelectedQuarter,
                PostalCode = dTO.ZipCode,
                FullAddress = dTO.FullAddress
            };


            dTO.Company = await _companyService.GetCompany(appUser.CompanyId);
            dTO.CompanyId = appUser.CompanyId;
            dTO.Address = address;
            int rowsChanged = await _appUserService.AddPersonnelAsync(dTO);
            if (rowsChanged != 0)
            {
                AppUser user = await _appUserService.GetUserWithIdentityAsync(dTO.IdentityNumber);
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("CreatePassword", "Login",
                    new { userId = user.Id, token = confirmationToken }, Request.Scheme
                    );
                string subject = "Create Password";
                string content = "Click here to create your password:  " + confirmationLink;
                SendEmail(user.Email, content, subject);

                ViewBag.ErrorTitle = "Registration successful";
                ViewBag.ErrorMessage = "Before you can Login, please create a password by clicking on the confirmation link we've sent to your email adress.";
            }
            return RedirectToAction("Index", "User");
        }
        [Route("{Controller}/{Action}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            AppUser appUser = await _signInManager.UserManager.GetUserAsync(User);
            List<AppUser> employeeList = _appUserService.GetAllUsersByCompanyId(appUser.CompanyId);

            return View(employeeList);
        }

        [Route("{Controller}/{Action}")]
        [HttpGet]
        public IActionResult ListExpenseRequests()
        {
            List<ExpenseRequest> expenses = _expenseRequestService.GetAll();

            return View(expenses);
        }

        private void SendEmail(string email, string content, string subject)
        {
            var message = new Message(new string[] { email }, subject, content);
            _emailService.SendEmail(message);
        }
    }
}