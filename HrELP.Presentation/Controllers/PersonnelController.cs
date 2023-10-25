using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.RequestCategoryService;
using HrELP.Application.Services.RequestTypeService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrELP.Presentation.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IExpenseRequestRepository _expenseRequestRepository;
        private readonly IRequestTypeService _typeService;
        private readonly IRequestCategoryService _requestCategoryService;
        private readonly ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;

        public PersonnelController(IAppUserService appUserService, SignInManager<AppUser> signInManager, IExpenseRequestRepository expenseRequestRepository, IRequestTypeService typeService, IRequestCategoryService requestCategoryService, ICompanyService companyService)
        {
            _appUserService = appUserService;
            _signInManager = signInManager;
            _expenseRequestRepository = expenseRequestRepository;
            _typeService = typeService;
            _requestCategoryService = requestCategoryService;
            _companyService = companyService;
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
        public async Task<IActionResult> FileARequest(RequestVM vM)
        {
            vM.AppUser= await _signInManager.UserManager.GetUserAsync(User);
            vM.RequestType= await _typeService.GetTypeById(vM.RequestType.Id);
            ExpenseRequest expenseRequest = new ExpenseRequest()
            {
                AppUser = vM.AppUser,
                ApprovalStatus = vM.ApprovalStatus,
                Currency = vM.Currency,
                Description = vM.Description,
                RequestTypeId=vM.RequestType.Id,
                RequestType=vM.RequestType,
                ExpenseAmount=vM.ExpenseAmount,
                CreateDate=DateTime.Now,
                IsActive=true,
                CompanyId=vM.AppUser.CompanyId,
            };

            await _expenseRequestRepository.AddAsync(expenseRequest);
            return RedirectToAction("Index","User");
        }
    }
}
