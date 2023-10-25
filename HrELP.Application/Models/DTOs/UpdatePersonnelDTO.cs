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
    public class UpdatePersonnelDTO
    {
        public string? Photo { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public int AddressId { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }
}
