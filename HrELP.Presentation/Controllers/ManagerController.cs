using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Application.Models.ViewModels;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.Services.AddressService;
using HrELP.Application.Services.AdvanceRequestService;
using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.ExpenseRequestService;
using HrELP.Application.Services.LeaveRequestService;
using HrELP.Application.Services.LeaveTypeService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure;
using HrELP.Infrastructure.Repositories;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.BuilderProperties;
using MimeKit;
using NETCore.MailKit.Core;
using System.Net;
using System.Net.Mail;
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
        private readonly IExpenseRequestService _expenseRequestService;
        private readonly IAdvanceRequestService _advanceRequestService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        #region SomeLists
        static List<string> departments = new List<string>
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

        static List<string> positions = new List<string>
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
        #endregion
        public ManagerController(SignInManager<AppUser> signInManager, AddressAPIService addressAPI, IAppUserService appUserService, ICompanyService companyService, IMapper mapper, UserManager<AppUser> userManager, IExpenseRequestService expenseRequestService, IWebHostEnvironment webHostEnvironment, IAdvanceRequestService advanceRequestService, ILeaveRequestService leaveRequestService, ILeaveTypeRepository leaveTypeRepository)
        {
            _signInManager = signInManager;
            _addressAPI = addressAPI;
            _appUserService = appUserService;
            _companyService = companyService;
            _mapper = mapper;
            _userManager = userManager;
            _expenseRequestService = expenseRequestService;
            _webHostEnvironment = webHostEnvironment;
            _advanceRequestService = advanceRequestService;
            _leaveRequestService = leaveRequestService;
            _leaveTypeRepository = leaveTypeRepository;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        [Route("{Controller}/{Action}")]
        [HttpGet]
        public async Task<IActionResult> AddPersonnel()
        {
           
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
            AppUser user2 = await _appUserService.GetUserWithIdentityAsync(dTO.IdentityNumber);
            if(user2 != null)
            {
                ViewData["IdentityExists"] = "This identity already exists";
                ViewBag.Departments = departments;
                ViewBag.Positions = positions;
                var cities = await _addressAPI.GetCitiesAsync();
                var cityList = cities.Select(x => new SelectListItem { Value = x, Text = x }).ToList();
                ViewBag.City = cityList;
                return View(dTO);
            }

            dTO.FirstName = CapitalizeFirstLetter(dTO.FirstName);
            dTO.SecondFirstName = CapitalizeFirstLetter(dTO.SecondFirstName);
            dTO.LastName = CapitalizeFirstLetter(dTO.LastName);
            dTO.SecondLastName = CapitalizeFirstLetter(dTO.SecondLastName);

            int rowsChanged = await _appUserService.AddPersonnelAsync(dTO);
            if (rowsChanged != 0)
            {
                AppUser user = await _appUserService.GetUserWithIdentityAsync(dTO.IdentityNumber);
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("CreatePassword", "Login",
                    new { userId = user.Id, token = confirmationToken }, Request.Scheme
                    );
                string HtmlBody = "";
                var PathToFile = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate", "Confirmation.html");

                var builder = new BodyBuilder();
                using (StreamReader sr = System.IO.File.OpenText(PathToFile))
                {
                    builder.HtmlBody = sr.ReadToEnd();
                }

                builder.HtmlBody = builder.HtmlBody.Replace("{0}", user.FullName);
                builder.HtmlBody = builder.HtmlBody.Replace("{1}", confirmationLink);

                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress("noreplyhrelp@gmail.com");
                mail.Subject = "Confirmation Link";
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
                        List<AppUser> employeeList = _appUserService.GetAllUsersByCompanyId(appUser.CompanyId);
                        ViewData["ErrorTitle"] = "Registration successful";
                        ViewData["ErrorMessage"] = "Before you can Login, please create a password by clicking on the confirmation link we've sent to your email adress.";
                        return View("GetEmployeeList", employeeList);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }
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
        
        public async Task<IActionResult> ApproveAdvanceRequest(int id)
        {
            AdvanceRequest advanceRequest = await _advanceRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(advanceRequest.UserId);
            advanceRequest.UpdateDate = DateTime.Now;
            advanceRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Approved;
            advanceRequest.ResponseDate = DateTime.Now;
            if (advanceRequest.RequestType.Id == 11)
            {
                appUser.AdvanceLimit = appUser.AdvanceLimit - advanceRequest.RequestAmount;
                await _appUserService.UpdateAsync(appUser);
            }
            advanceRequest.IsActive = false;
            await _advanceRequestService.UpdateAsync(advanceRequest);
            SendEmailByAdvanceStatus(id);

            return RedirectToAction(nameof(ListAdvanceRequests));
        }
        public async Task<IActionResult> RefuseAdvanceRequest(int id)
        {
            AdvanceRequest advanceRequest = await _advanceRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(advanceRequest.UserId);
            advanceRequest.UpdateDate = DateTime.Now;
            advanceRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Declined;
            advanceRequest.ResponseDate = DateTime.Now;
            advanceRequest.IsActive = false;
            await _advanceRequestService.UpdateAsync(advanceRequest);

            SendEmailByAdvanceStatus(id);

            return RedirectToAction(nameof(ListAdvanceRequests));
        }
        public async Task<IActionResult> ApproveExpenseRequest(int id)
        {
            ExpenseRequest expenceRequest = await _expenseRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(expenceRequest.UserId);
            expenceRequest.UpdateDate = DateTime.Now;
            expenceRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Approved;
            expenceRequest.ResponseDate = DateTime.Now;
            expenceRequest.IsActive = false;
            await _expenseRequestService.UpdateAsync(expenceRequest);

            SendEmailByExpenseStatus(id);

            return RedirectToAction(nameof(ListExpenseRequests));
        }
        public async Task<IActionResult> RefuseExpenseRequest(int id)
        {
            ExpenseRequest expenceRequest = await _expenseRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(expenceRequest.UserId);
            expenceRequest.UpdateDate = DateTime.Now;
            expenceRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Declined;
            expenceRequest.ResponseDate = DateTime.Now;
            expenceRequest.IsActive = false;
            await _expenseRequestService.UpdateAsync(expenceRequest);

            SendEmailByExpenseStatus(id);

            return RedirectToAction(nameof(ListExpenseRequests));
        }
        public async Task<IActionResult> ExpenseRequestDetail(int id)
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
            };
            return PartialView("ExpenseRequestDetails", requestVM);
        }
        public IActionResult ListLeaveRequests()
        {
            List<LeaveRequest> leaveRequests = _leaveRequestService.GetAll();

            return View(leaveRequests);
        }
        public async Task<IActionResult> LeaveRequestDetails(int id)
        {
            LeaveRequest request = await _leaveRequestService.GetRequestById(id);
            int leaveTypeId = request.LeaveTypeId;
            LeaveTypes leaveType = await _leaveTypeRepository.GetLeaveTypeById(leaveTypeId);
            request.LeaveType = leaveType;
            LeaveRequestVM requestVM = new LeaveRequestVM()
            {
                AppUser = request.AppUser,
                UserId = request.UserId,
                Id = request.Id,
                Description = request.Description,
                CreateDate = request.CreateDate.Value,
                UpdateDate = request.UpdateDate.Value,
                RequestTypes = request.RequestType,
                RequestTypeId = request.RequestTypeId.Value,
                TotalDaysOff = request.LeaveType.DayValue,
                ApprovalStatus = request.ApprovalStatus,
                LeaveType = request.LeaveType

            };
            return View(requestVM);
        }
        public async Task<IActionResult> LeaveRefuseRequest(int id)//
        {
            LeaveRequest leaveRequest = await _leaveRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(leaveRequest.UserId);
            leaveRequest.UpdateDate = DateTime.Now;
            leaveRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Declined;
            leaveRequest.ResponseDate = DateTime.Now;
            leaveRequest.ReplyDate = DateTime.Now;
            leaveRequest.IsActive = false;
            await _leaveRequestService.UpdateAsync(leaveRequest);
            SendEmailByStatus(id);
            return RedirectToAction(nameof(ListLeaveRequests));
        }
        public async Task<IActionResult> LeaveApproveRequest(int id)//
        {
            LeaveRequest leaveRequest = await _leaveRequestService.GetRequestById(id);
            AppUser appUser = await _appUserService.GetUserAsync(leaveRequest.UserId);
            leaveRequest.UpdateDate = DateTime.Now;
            leaveRequest.ApprovalStatus = Domain.Entities.Enums.ApprovalStatus.Approved;
            leaveRequest.ResponseDate = DateTime.Now;
            leaveRequest.ReplyDate = DateTime.Now;
            leaveRequest.IsActive = false;
            await _leaveRequestService.UpdateAsync(leaveRequest);
            SendEmailByStatus(id);
            return RedirectToAction(nameof(ListLeaveRequests));
        }
        public async Task SendEmailByStatus(int id)//roottacshtml
        {
            LeaveRequest lr = await _leaveRequestService.GetRequestById(id);

            //LeaveRequest request = await _leaveRequestService.GetRequestById(id);
            //request.LeaveType = await _leaveTypeService.GetLeaveTypeAsync(id);
            //user.Id = lr.UserId;
            string HtmlBody = "";
            var PathToFile = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate", "SendEmailByStatus.html");

            var builder = new BodyBuilder();
            using (StreamReader sr = System.IO.File.OpenText(PathToFile))
            {
                builder.HtmlBody = sr.ReadToEnd();
            }

            builder.HtmlBody = builder.HtmlBody.Replace("{0}", lr.AppUser.FullName);
            builder.HtmlBody = builder.HtmlBody.Replace("{1}", lr.CreateDate.ToString());//requesttypname
            builder.HtmlBody = builder.HtmlBody.Replace("{2}", lr.ReplyDate.ToString());//cevaplanmaTarihi
            builder.HtmlBody = builder.HtmlBody.Replace("{3}", lr.ApprovalStatus.ToString());//OnaylamaStatus
            builder.HtmlBody = builder.HtmlBody.Replace("{4}", lr.Description);

            MailMessage mail = new MailMessage();
            mail.To.Add(lr.AppUser.Email);
            mail.From = new MailAddress("noreplyhrelp@gmail.com");
            mail.Subject = "Request Information";
            mail.Body = builder.HtmlBody;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("noreplyhrelp@gmail.com", "rmzqiazgoktnbcac");
                smtp.Send(mail);

            }
        }
        public async Task SendEmailByExpenseStatus(int id)//roottacshtml
        {
            ExpenseRequest er = await _expenseRequestService.GetRequestById(id);

            //LeaveRequest request = await _leaveRequestService.GetRequestById(id);
            //request.LeaveType = await _leaveTypeService.GetLeaveTypeAsync(id);
            //user.Id = lr.UserId;
            string HtmlBody = "";
            var PathToFile = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate", "SendEmailByStatus.html");

            var builder = new BodyBuilder();
            using (StreamReader sr = System.IO.File.OpenText(PathToFile))
            {
                builder.HtmlBody = sr.ReadToEnd();
            }

            builder.HtmlBody = builder.HtmlBody.Replace("{0}", er.AppUser.FullName);
            builder.HtmlBody = builder.HtmlBody.Replace("{1}", er.CreateDate.ToString());//requesttypname
            builder.HtmlBody = builder.HtmlBody.Replace("{2}", er.ResponseDate.ToString());//cevaplanmaTarihi
            builder.HtmlBody = builder.HtmlBody.Replace("{3}", er.ApprovalStatus.ToString());//OnaylamaStatus
            builder.HtmlBody = builder.HtmlBody.Replace("{4}", er.Description);

            MailMessage mail = new MailMessage();
            mail.To.Add(er.AppUser.Email);
            mail.From = new MailAddress("noreplyhrelp@gmail.com");
            mail.Subject = "Request Information";
            mail.Body = builder.HtmlBody;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("noreplyhrelp@gmail.com", "rmzqiazgoktnbcac");
                smtp.Send(mail);

            }
        }
        public async Task SendEmailByAdvanceStatus(int id)//roottacshtml
        {
            AdvanceRequest ar = await _advanceRequestService.GetRequestById(id);

            //LeaveRequest request = await _leaveRequestService.GetRequestById(id);
            //request.LeaveType = await _leaveTypeService.GetLeaveTypeAsync(id);
            //user.Id = lr.UserId;
            string HtmlBody = "";
            var PathToFile = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate", "SendEmailByStatus.html");

            var builder = new BodyBuilder();
            using (StreamReader sr = System.IO.File.OpenText(PathToFile))
            {
                builder.HtmlBody = sr.ReadToEnd();
            }

            builder.HtmlBody = builder.HtmlBody.Replace("{0}", ar.AppUser.FullName);
            builder.HtmlBody = builder.HtmlBody.Replace("{1}", ar.CreateDate.ToString());//requesttypname
            builder.HtmlBody = builder.HtmlBody.Replace("{2}", ar.ResponseDate.ToString());//cevaplanmaTarihi
            builder.HtmlBody = builder.HtmlBody.Replace("{3}", ar.ApprovalStatus.ToString());//OnaylamaStatus
            builder.HtmlBody = builder.HtmlBody.Replace("{4}", ar.Description);

            MailMessage mail = new MailMessage();
            mail.To.Add(ar.AppUser.Email);
            mail.From = new MailAddress("noreplyhrelp@gmail.com");
            mail.Subject = "Request Information";
            mail.Body = builder.HtmlBody;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("noreplyhrelp@gmail.com", "rmzqiazgoktnbcac");
                smtp.Send(mail);

            }

        }
        public string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string firstLetter = input.Substring(0, 1).ToUpper();
            string restOfTheString = input.Substring(1).ToLower();

            return firstLetter + restOfTheString;
        }
    }
}