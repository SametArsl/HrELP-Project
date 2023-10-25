using HrELP.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Entities.Concrete
{
    public class Address : IBaseEntity
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? District { get; set; }
        public string? Quarter { get; set; }
        public string FullAddress { get; set; }
        public int PostalCode { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public override string ToString()
        {
            return $"{City} {Town} {District} {Quarter} {FullAddress} {PostalCode}";
        }
    }
}
