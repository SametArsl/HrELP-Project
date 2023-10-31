﻿using HrELP.Application.Services.AdvanceRequestService;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public PersonnelController(IAppUserService appUserService, SignInManager<AppUser> signInManager, IExpenseRequestRepository expenseRequestRepository, IRequestTypeService typeService, IRequestCategoryService requestCategoryService, ICompanyService companyService, IWebHostEnvironment webHostEnvironment, IAdvanceRequestService advanceRequestService)
        {

            _appUserService = appUserService;
            _signInManager = signInManager;
            _expenseRequestRepository = expenseRequestRepository;
            _typeService = typeService;
            _requestCategoryService = requestCategoryService;
            _companyService = companyService;
            _webHostEnvironment = webHostEnvironment;
            _advanceRequestService = advanceRequestService;
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

            await _expenseRequestRepository.AddAsync(expenseRequest);
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult AdvanceRequest()
        {
            ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AdvanceRequest(AdvanceRequestVM vM)
        {
            vM.AppUser = await _signInManager.UserManager.GetUserAsync(User);
            vM.RequestType = await _typeService.GetTypeById(vM.RequestType.Id);
            if (vM.AppUser.AdvanceLimit >= vM.AdvanceAmount)
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


                try
                {
                    await _advanceRequestService.CreateRequest(advanceRequest);
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = $"The error occurred. Error Message={ex.Message}";
                }
                
                return RedirectToAction("Index", "User");
        }
            else
            {
                ViewBag.Requests = _typeService.GetAdvanceRequestTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RequestName }).ToList();
                ViewData["OutOfLimit"] = $"The requested advance amount exceeds the maximum advance limit. You can withdraw up to {vM.AppUser.AdvanceLimit} TL at most.";
                return View(vM);
    }
}
    }
}
