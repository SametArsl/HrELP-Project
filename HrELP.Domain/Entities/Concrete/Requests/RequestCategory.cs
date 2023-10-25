using HrELP.Domain.Entities.Abstract;
using HrELP.Domain.Entities.Enums;

namespace HrELP.Domain.Entities.Concrete.Requests
{
    public class RequestCategory : IBaseEntity
    {
        public RequestCategory()
        {
            RequestTypes = new HashSet<RequestType>();
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RequestType> RequestTypes { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}