namespace HrELP.Presentation.Models.ViewModels
{
    public class UserDetailBaseVM
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
