using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Domain.Entities.Concrete;
using HrELP.Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.AutoMapper
{
    public class HrELPMapper : Profile
    {
        public HrELPMapper()
        {
            CreateMap<AppUser, AddPersonnelDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, AddPersonnelDTO>().ReverseMap();
            CreateMap<AppUser, UpdatePersonnelDTO>().ReverseMap();
            CreateMap<AppUser, UpdateUserDTO>().ReverseMap();   
            CreateMap<AppUser, UserDetailVM>().ReverseMap();   
            CreateMap<AppUser, UserDetailBaseVM>().ReverseMap();
            CreateMap<UpdateUserDTO, UserDetailVM>().ReverseMap();
        }
    }
}
