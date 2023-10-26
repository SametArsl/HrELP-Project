using HrELP.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace HrELP.Presentation.Models.ViewModels
{
    public class UserDetailVM:UserDetailBaseVM
    {
        public string ImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string FullAddress { get; set; }
        public int ZipCode { get; set; }
        public string BirthPlace { get; set; } 
        public string SelectedCity { get; set; }
        public string SelectedTown { get; set; }
        public string SelectedDistrict { get; set; }
        public string SelectedQuarter { get; set; }
        public IFormFile? PhotoFile { get; set; }
    }
}
