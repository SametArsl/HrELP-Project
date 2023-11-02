using HrELP.Application.Models.ViewModels;
using HrELP.Application.Services.AdvanceRequestService;
using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.ExpenseRequestService;
using HrELP.Application.Services.LeaveRequestService;
using HrELP.Application.Services.LeaveTypeService;
using HrELP.Application.Services.RequestCategoryService;
using HrELP.Application.Services.RequestTypeService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using HrELP.Domain.Repositories;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LeaveRequestsVM = HrELP.Application.Models.ViewModels.LeaveRequestsVM;

namespace HrELP.Presentation.Controllers
{
    [Authorize(Roles = "Personnel")]
    public class PersonnelController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IExpenseRequestRepository _expenseRequestRepository;
        private readonly IRequestTypeService _typeService;
        private readonly IRequestCategoryService _requestCategoryService;
        private readonly ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdvanceRequestService _advanceRequestService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IExpenseRequestService _expenseRequestService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        private readonly ILeaveTypeService _leaveTypeService;

        public PersonnelController(IAppUserService appUserService, SignInManager<AppUser> signInManager, IExpenseRequestRepository expenseRequestRepository, IRequestTypeService typeService, IRequestCategoryService requestCategoryService, ICompanyService companyService, IWebHostEnvironment webHostEnvironment, IAdvanceRequestService advanceRequestService, ILeaveRequestService leaveRequestService, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, ILeaveTypeService leaveTypeService, IExpenseRequestService expenseRequestService)
        {

            _appUserService = appUserService;
            _signInManager = signInManager;
            _expenseRequestRepository = expenseRequestRepository;
            _typeService = typeService;
            _requestCategoryService = requestCategoryService;
            _companyService = companyService;
            _webHostEnvironment = webHostEnvironment;
            _advanceRequestService = advanceRequestService;
            _leaveRequestService = leaveRequestService;
            _userManager = userManager;
            _leaveTypeService = leaveTypeService;
            _expenseRequestService = expenseRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FileARequest()
        {
            ViewBag.Requests = _typeService.GetExpenseRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FileARequest(ExpenseRequestVM vM)
        {
            vM.AppUser = await _signInManager.UserManager.GetUserAsync(User);
            vM.RequestType = await _typeService.GetTypeById(vM.RequestType.Id);
            ExpenseRequest expenseRequest = new ExpenseRequest()
            {
                AppUser = vM.AppUser,
                ApprovalStatus = vM.ApprovalStatus,
                Currency = vM.Currency,
                Description = vM.Description,
                RequestTypeId = vM.RequestType.Id,
                RequestType = vM.RequestType,
                ExpenseAmount = vM.ExpenseAmount,
                CreateDate = DateTime.Now,
                IsActive = true,
                CompanyId = vM.AppUser.CompanyId,
            };

            if (vM.FormFile != null)
            {
                IFormFile formFile = vM.FormFile;
                var extent = Path.GetExtension(formFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "requestFiles", randomName);
                vM.FilePath = randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            expenseRequest.FilePath = vM.FilePath;
            if (_expenseRequestService.PendingRequests(vM.AppUser).Count > 0)
            {
                ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
                ViewData["OutOfLimit"] = "The system allows only one expence request per category to be pending at the same time.";
                return View(vM);
            }
            else
            {
                try
                {
                    await _expenseRequestRepository.AddAsync(expenseRequest);
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = $"The error occurred. Error Message={ex.Message}";
                }
            }
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public async Task<IActionResult> AdvanceRequest()
        {
            ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
            AppUser user = await _signInManager.UserManager.GetUserAsync(User);
            AdvanceRequestVM vm = new AdvanceRequestVM()
            {
                AdvanceLimit = user.AdvanceLimit
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AdvanceRequest(AdvanceRequestVM vM)
        {
            vM.AppUser = await _signInManager.UserManager.GetUserAsync(User);
            vM.RequestType = await _typeService.GetTypeById(vM.RequestType.Id);
            if (vM.AppUser.AdvanceLimit >= vM.AdvanceAmount)
            {
                if(vM.RequestType != null)
                {
                    AdvanceRequest advanceRequest = new AdvanceRequest()
                    {
                        AppUser = vM.AppUser,
                        ApprovalStatus = vM.ApprovalStatus,
                        Currency = vM.Currency,
                        Description = vM.Description,
                        RequestTypeId = vM.RequestType.Id,
                        RequestType = vM.RequestType,
                        RequestAmount = vM.AdvanceAmount,
                        CreateDate = DateTime.Now,
                        IsActive = true,
                        CompanyId = vM.AppUser.CompanyId,
                    };

                    if (_advanceRequestService.PendingRequests(vM.AppUser).Count > 0)
                    {
                        ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
                        ViewData["OutOfLimit"] = "The system allows only one advance request per category to be pending at the same time.";
                        return View(vM);
                    }
                    else
                    {
                        try
                        {
                            await _advanceRequestService.CreateRequest(advanceRequest);
                        }
                        catch (Exception ex)
                        {
                            ViewData["Message"] = $"The error occurred. Error Message={ex.Message}";
                        }

                        return RedirectToAction(nameof(ListAdvanceRequest));
                    }

                }

                else
                {
                    ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
                    ViewData["OutOfLimit"] = "Please select a request type !";
                    return View(vM);
                }
            }
            else
            {
                ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
                ViewData["OutOfLimit"] = $"The requested advance amount exceeds the maximum advance limit. You can withdraw up to {vM.AppUser.AdvanceLimit} TL at most.";
                return View(vM);
            }
        }

        [HttpGet]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> CreateLeaveRequest()
        {
            LeaveRequestsVM lrm = new LeaveRequestsVM();
            lrm.Personnel = await _signInManager.UserManager.GetUserAsync(User);
            lrm.LeaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            if (lrm.Personnel != null)
            {
                lrm.PersonnelId = lrm.Personnel.Id;
                return View(lrm);
            }
            else
            {
                return RedirectToAction("Error", "Login");
            }

        }

        [HttpPost]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> CreateLeaveRequest(LeaveRequestsVM leaveRequestVM)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            leaveRequestVM.Personnel = user;
            leaveRequestVM.pendingLeaveList = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Pending_Approval, user.Id);
            LeaveTypes leaveType = await _leaveTypeService.GetLeaveTypeAsync(leaveRequestVM.LeaveTypeId);
            decimal leavedays = leaveRequestVM.EndDate.Subtract(leaveRequestVM.StartDate).Days;
            LeaveRequest leaveRequest = new LeaveRequest()
            {
                ApprovalStatus = ApprovalStatus.Pending_Approval,
                AppUser = user,
                LeaveType = leaveType,
                IsActive = true,
                CompanyId = user.CompanyId,
                CreateDate = DateTime.Now,
                Description = leaveRequestVM.Description,
                EndDate = leaveRequestVM.EndDate,
                StartDate = leaveRequestVM.StartDate,
                LeaveDayNumber = leavedays,
                UserId = user.Id,
                RequestDate = DateTime.Now,
                RequestTypeId=12,
                LeaveTypeId = leaveRequestVM.LeaveTypeId,
            };
            user.RemainingLeaveRight = user.RemainingLeaveRight - Convert.ToDecimal(leavedays);
            foreach (var item in leaveRequestVM.pendingLeaveList)
            {

                if (item.LeaveTypeId == leaveRequest.LeaveTypeId)
                {
                    LeaveListVM leaveListVMs = new LeaveListVM();
                    leaveListVMs.Personnel = user;
                    leaveListVMs.PendingLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Pending_Approval, user.Id, p => p.LeaveType);
                    leaveListVMs.ApprovedLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Approved, user.Id, p => p.LeaveType);
                    leaveListVMs.DeclinedLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Declined, user.Id, p => p.LeaveType);
                    leaveListVMs.ErrorMessage = "Already Exist Leave Request";
                    return View("ListLeaveRequests", leaveListVMs);

                }
            }

            await _appUserService.UpdateAsync(user);
            await _leaveRequestService.CreateLeaveRequestAsync(leaveRequest);
            return RedirectToAction("ListLeaveRequests");
        }
        [HttpGet]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> ListLeaveRequests()
        {
            LeaveListVM leaveListVM = new LeaveListVM();
            AppUser user = await _userManager.GetUserAsync(User);
            leaveListVM.Personnel = user;
            leaveListVM.PendingLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Pending_Approval, user.Id, p => p.LeaveType);
            leaveListVM.ApprovedLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Approved, user.Id, p => p.LeaveType);
            leaveListVM.DeclinedLeaveRequest = await _leaveRequestService.GetLeaveRequestByStatusAsync(ApprovalStatus.Declined, user.Id, p => p.LeaveType);

            return View(leaveListVM);
        }
        [HttpPost]
        [Route("{Controller}/{Action}")]
        public async Task<IActionResult> ListLeaveRequests(int id)
        {
            await _leaveRequestService.DeniedLeaveRequestAsync(id);
            return View("ListLeaveRequests");
        }
        public async Task<IActionResult> ListAdvanceRequest()
        {
            AppUser appUser= await _userManager.GetUserAsync(User);
            var list =  _advanceRequestService.AllRequests(appUser);
            return View(list);
        }
        public async Task<IActionResult> ListExpenseRequest()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var list = _expenseRequestService.AllRequests(appUser);
            return View(list);
        }
        public async Task<IActionResult> DeleteAdvanceRequest(int id)
        {
            var advanceRequest = await _advanceRequestService.GetRequestById(id);
            await _advanceRequestService.DeleteAsync(advanceRequest);

            return RedirectToAction(nameof(ListAdvanceRequest));
        }
        public async Task<IActionResult> DeleteExpenseRequest(int id)
        {
            var expenseRequest = await _expenseRequestService.GetRequestById(id);
            await _expenseRequestService.DeleteAsync(expenseRequest);

            return RedirectToAction(nameof(ListExpenseRequest));
        }
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await _leaveRequestService.GetRequestById(id);
            await _leaveRequestService.DeleteAsync(leaveRequest);

            return RedirectToAction(nameof(ListLeaveRequests));
        }
    }
}
