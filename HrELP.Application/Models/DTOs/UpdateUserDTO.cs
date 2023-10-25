using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Models.DTOs
{
    public class UpdateUserDTO
    {
        public string? UserName { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string? PhoneNumber { get; set; }
        public string FullAddress { get; set; }
        public int ZipCode { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedTown { get; set; }
        public string SelectedDistrict { get; set; }
        public string SelectedQuarter { get; set; }
    }
}
