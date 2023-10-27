using HrELP.Domain.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace HrELP.Presentation.Models.ViewModels
{  
    public class CreatePasswordVM
    {
        public int UserId { get; set; }        

		[Required]
		public string Password { get; set; }
		[Required]
		public string ReTypePassword { get; set; }
        public string Token { get; set; }
    }
}
