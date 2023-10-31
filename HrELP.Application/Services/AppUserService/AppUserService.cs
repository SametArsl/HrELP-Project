using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure;
using HrELP.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserRepository userRepository, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<int> AddPersonnelAsync(AddPersonnelDTO addPersonnelDTO)
        {
            AppUser user = _mapper.Map<AppUser>(addPersonnelDTO);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.Email = ConvertToEmail(user.FirstName,user.LastName, user.Company.CompanyName);
            if (IsEmailInUse(user.Email))
            {
                user.Email = GenerateUniqueEmail(user.Email);
            }
            user.UserName = user.Email;
            user.NormalizedEmail = ConvertTurkishCharacters(user.Email).ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.NormalizedEmail;

            await _userRepository.AddAsync(user);
            await _userManager.AddToRoleAsync(user, "Personnel");
            user.AddressId = user.Address.Id;           
            return await _userRepository.UpdateAsync(user);
        }

        public List<AppUser> GetAllUsersByCompanyId(int id)
        {
            var allUsers =  _userRepository.GetAll().Where(x=>x.CompanyId == id).ToList();
            return allUsers;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO loginDTO)
        {
            AppUser user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserName == loginDTO.UserName);

            await _signInManager.SignOutAsync();

            return await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        private static string ConvertTurkishCharacters(string text)
        {
            text = text.Replace("Ç", "C").Replace("ç", "c")
                       .Replace("İ", "I").Replace("ı", "i")
                       .Replace("Ğ", "G").Replace("ğ", "g")
                       .Replace("Ö", "O").Replace("ö", "o")
                       .Replace("Ş", "S").Replace("ş", "s")
                       .Replace("Ü", "U").Replace("ü", "u");
            return text;
        }
        public static string ConvertToEmail(string firstName, string lastName, string companyName)
        {
            string convertedName = ConvertTurkishCharacters(firstName).ToLower();
            string convertedLastName = ConvertTurkishCharacters(lastName).ToLower();
            string convertedCompany = companyName.Replace(" ", "");


            string email = $"{convertedName}.{convertedLastName}@{convertedCompany.ToLower()}.com";
            return email;
        }
        public bool IsEmailInUse(string email)
        {
            var user = GetAllUsersByEmail(email);
            if (user.Count < 1)
            {
                return false;
            }
            return true;
        }
        public string GenerateUniqueEmail(string email, int attempt = 1)
        {
            string[] parts = email.Split('@');
            string newEmail = attempt == 1 ? $"{parts[0]}{attempt}@{parts[1]}" : $"{parts[0]}{attempt}@{parts[1]}";

            if (IsEmailInUse(newEmail))
            {
                return GenerateUniqueEmail(email, attempt + 1);
            }

            return newEmail;
        }
        public async Task<AppUser> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return user;
        }

        public async Task<int> UpdateAsync(UpdateUserDTO updateUserDTO)
        {
            AppUser user = await _userRepository.GetFirstOrDefaultAsync(x=>x.UserName == updateUserDTO.UserName);
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            user.Photo = updateUserDTO.Photo;
            _mapper.Map(user,updateUserDTO);
            return await _userRepository.UpdateAsync(user);
        }

        public List<AppUser> GetAllUsersByEmail(string email)
        {
            var allUsers = _userRepository.GetAll().Where(x => x.Email == email).ToList();
            return allUsers;
        }

        public async Task<AppUser> GetUserWithIdentityAsync(string identity)
        {
            AppUser user = await _userRepository.GetFirstOrDefaultAsync(x => x.IdentityNumber == identity);
            return user;
        }

        public async Task<AppUser> GetRequestsWithUserAndCompany(int id)
        {
            AppUser user = await _userRepository.GetUserWithRequestsAsync(id);
            return user;
        }

        public async Task<AppUser> GetUserWithEmail(string email)
        {
            AppUser user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserName == email);
            return user;
        }
           public async Task<AppUser> GetUserWithEmailAsync(ForgetPasswordVM vm)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email ==vm.Email);
            return user;
        }
        
        public async Task<AppUser> GetUserWithEmailAsync(CreatePasswordVM vm)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Id == vm.UserId);
            return user;
        }

        public async Task<int> UpdateAsync(AppUser appUser)
        {
            return await _userRepository.UpdateAsync(appUser);
        }
    }
}
