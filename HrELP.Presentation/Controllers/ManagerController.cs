using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.Services.AddressService;
using HrELP.Application.Services.AdvanceRequestService;
using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.EmailService;
using HrELP.Application.Services.ExpenseRequestService;
using HrELP.Application.Services.LeaveRequestService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Infrastructure;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.BuilderProperties;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
        private readonly IAdvanceRequestService _advanceRequestService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManagerController(SignInManager<AppUser> signInManager, AddressAPIService addressAPI, IAppUserService appUserService, ICompanyService companyService, IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService, IExpenseRequestService expenseRequestService, IWebHostEnvironment webHostEnvironment, IAdvanceRequestService advanceRequestService, ILeaveRequestService leaveRequestService)
        {
            _signInManager = signInManager;
            _addressAPI = addressAPI;
            _appUserService = appUserService;
            _companyService = companyService;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _expenseRequestService = expenseRequestService;
            _webHostEnvironment = webHostEnvironment;
            _advanceRequestService = advanceRequestService;
            _leaveRequestService = leaveRequestService;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        [Route("{Controller}/{Action}")]
        [HttpGet]
        public async Task<IActionResult> AddPersonnel()
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



            // ViewBag'e departmanları ve pozisyonları ekle
            ViewBag.Departments = departments;
            ViewBag.Positions = positions;
            var cities = await _addressAPI.GetCitiesAsync();
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
            if (dTO.PhotoFile != null)
            {
                IFormFile formFile = dTO.PhotoFile;
                var extent = Path.GetExtension(formFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profilepic", randomName);
                dTO.Photo = randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

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
        [Route("{Controller}/{Action}")]
        [HttpGet]
        public async Task<IActionResult> PersonelDetails(int id)
        {
            AppUser user = await _appUserService.GetUserAsync(id);
            UserDetailVM userDetailsVM = new UserDetailVM();
            _mapper.Map(user, userDetailsVM);
            userDetailsVM.FullAddress = user.Address.FullAddress;
            return View(userDetailsVM);
        }
        public async Task<IActionResult> ExpenseRequestDetails(int id)
        {
            ExpenseRequest request = await _expenseRequestService.GetRequestById(id);
            ExpenseRequestVM requestVM = new ExpenseRequestVM()
            {
                ApprovalStatus = request.ApprovalStatus,
                AppUser = request.AppUser,
                ExpenseAmount = request.ExpenseAmount,
                Currency = request.Currency,
                Description = request.Description,
                Id = request.Id,
                RequestType = request.RequestType,
                FilePath = request.FilePath,
            };
            return PartialView("RequestDetails", requestVM);
        }
        [HttpGet]
        public IActionResult ListAdvanceRequests()
        {
            List<AdvanceRequest> advances = _advanceRequestService.GetAll();

            return View(advances);
        }
        public async Task<IActionResult> AdvanceRequestDetails(int id)
        {
            AdvanceRequest request = await _advanceRequestService.GetRequestById(id);
            AdvanceRequestVM requestVM = new AdvanceRequestVM()
            {
                ApprovalStatus = request.ApprovalStatus,
                AppUser = request.AppUser,
                AdvanceAmount = request.RequestAmount,
                Currency = request.Currency,
                Description = request.Description,
                Id = request.Id,
                RequestType = request.RequestType,
            };
            return PartialView("AdvanceRequestDetails", requestVM);
        }
        [HttpGet]
        public IActionResult ListLeaveRequests()
        {
            List<LeaveRequest> leaveRequests = _leaveRequestService.GetAll();

            return View(leaveRequests);
        }
        public async Task<IActionResult> LeaveRequestDetails(int id)
        {
            LeaveRequest request = await _leaveRequestService.GetRequestById(id);
            LeaveRequestVM requestVM = new LeaveRequestVM()
            {
                ApprovalStatus = request.ApprovalStatus,
                AppUser = request.AppUser,
                TotalDaysOff = request.TotalDaysOff,
                Description = request.Description,
                Id = request.Id,
                RequestType = request.RequestType,
            };
            return PartialView("LeaveRequestDetails", requestVM);
        }
        public async Task<IActionResult> ApproveRequest(int id)
        {
            AdvanceRequest advanceRequest = await _advanceRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(advanceRequest.UserId);
            advanceRequest.UpdateDate = DateTime.Now;
            advanceRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Approved;
            if (advanceRequest.RequestType.Id == 6)
            {
                appUser.AdvanceLimit = appUser.AdvanceLimit - advanceRequest.RequestAmount;
            }
            return RedirectToAction("ListAdvanceRequests");
        }
        public IActionResult RequestDetail(ExpenseRequestVM vm)
        {
            return PartialView(vm);
        }
    }
}