using System.ComponentModel.DataAnnotations;

namespace HrELP.Presentation.Models.ViewModels
{  
    public class CreatePasswordVM
    {
        public string userId { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string ReTypePassword { get; set; }
        public string Token { get; set; }
    }
}
