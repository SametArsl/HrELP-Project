using HrELP.Application.Services.AppUserService;
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
        private readonly SignInManager<AppUser> _signInManager;

        public PersonnelController(IAppUserService appUserService, SignInManager<AppUser> signInManager, IExpenseRequestRepository expenseRequestRepository, IRequestTypeService typeService, IRequestCategoryService requestCategoryService)
        {
            _appUserService = appUserService;
            _signInManager = signInManager;
            _expenseRequestRepository = expenseRequestRepository;
            _typeService = typeService;
            _requestCategoryService = requestCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FileARequest()
        {
            ViewBag.Requests = _typeService.GetExpenseRequestTypes().Select(x => new SelectListItem { Value = x.RequestCategoryId.ToString(), Text = x.RequestName }).ToList();

            return View();
        }

        // Umur Buraya Bak !!

        //[HttpPost]
        //public async Task<IActionResult> FileARequest(RequestVM vM)
        //{
        //    vM.AppUser = await _signInManager.UserManager.GetUserAsync(User);
        //    vM.RequestCategory = await _requestCategoryService.GetCategoryById(Convert.ToInt32(vM.RequestCategoryId));

        //    ExpenseRequest expenseRequest = new ExpenseRequest()
        //    {
        //        AppUser = vM.AppUser,
        //        ApprovalStatus = vM.ApprovalStatus,
        //        Currency = vM.Currency,
        //        Description = vM.Description,
        //         = vM.RequestCategoryId,
        //    };

        //    await _expenseRequestRepository.AddAsync(expenseRequest);
        //    return View();
        //}
    }
}
