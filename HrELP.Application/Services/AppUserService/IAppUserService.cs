using HrELP.Application.Models.DTOs;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<SignInResult> LoginAsync(LoginDTO loginDTO);
        Task<int> AddPersonnelAsync(AddPersonnelDTO addPersonnelDTO);
        List<AppUser> GetAllUsersByCompanyId(int id);
        Task Logout();
        bool IsEmailInUse(string email);
        Task<AppUser> GetUserAsync(int id);
        Task<int> UpdateAsync(UpdateUserDTO updateUserDTO);
        Task<int> UpdateAsync(AppUser appUser);
        List<AppUser> GetAllUsersByEmail(string email);
        Task<AppUser> GetUserWithIdentityAsync(string identity);
        Task<AppUser> GetRequestsWithUserAndCompany(int id);
        Task<AppUser> GetUserWithEmail(string email);
        Task<AppUser> GetUserWithEmailAsync(ForgetPasswordVM vm);
         Task<AppUser> GetUserWithEmailAsync(CreatePasswordVM vm);     
    }
}
