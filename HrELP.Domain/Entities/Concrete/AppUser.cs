using HrELP.Domain.Entities.Abstract;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        public AppUser()
        {
            AdvanceRequests = new HashSet<AdvanceRequest>();
            ExpenseRequests = new HashSet<ExpenseRequest>();
            LeaveRequests = new HashSet<LeaveRequest>();
        }
        public string? Photo { get; set; }
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        private string _fullName; // Özel bir alan (field) kullanıyoruz.
        public DateTime BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public DateTime? DismissalDate { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public string? Profession { get; set; }
         public decimal RemainingLeaveRight { get; set; }
        public bool? MarriageStatus { get; set; }
        public string Department { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        private decimal _salary;
        public decimal Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary= value;
                AdvanceLimit = value * 3;
            }
        }
        public int MaxDaysOff { get; set; }
        private decimal _workingTime;

        public int DayOffsLeft { get; set; }
        private decimal _advanceLimit;
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public ICollection<AdvanceRequest>? AdvanceRequests { get; set; }
        public ICollection<ExpenseRequest>? ExpenseRequests { get; set; }
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                if (SecondFirstName != null)
                {
                    if (SecondLastName != null)
                    {
                        return $"{FirstName} {SecondFirstName} {LastName} {SecondLastName}";
                    }
                    else
                    {
                        return $"{FirstName} {SecondFirstName} {LastName}";
                    }
                }
                else if (SecondLastName != null)
                {
                    return $"{FirstName} {LastName} {SecondLastName}";
                }
                else
                {
                    return $"{FirstName} {LastName}";
                }
            }
            set
            {
                _fullName = value;
            }
        }
        public decimal? AdvanceLimit { get; set; }

    }
}
