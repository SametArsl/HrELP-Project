using HrELP.Application.Extensions;
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
    public class AddPersonnelDTO
    {
        public string? Photo { get; set; }
        public IFormFile? PhotoFile { get; set; }
        [LetterValidator]
        public string FirstName { get; set; }
        [LetterValidator]
        public string? SecondFirstName { get; set; }
        [LetterValidator]
        public string LastName { get; set; }
        [LetterValidator]
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        [LetterValidator]
        public string BirthPlace { get; set; }
        public Gender Gender { get; set; }
        [NumberValidator]
        public string IdentityNumber { get; set; }
        public bool MarriageStatus { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public int AddressId { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
        [NumberValidator]
        public decimal Salary { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedTown { get; set; }
        public string SelectedDistrict { get; set; }
        public string? SelectedQuarter { get; set; }
        public string FullAddress { get; set; }
        public int ZipCode { get; set; }
    }
}