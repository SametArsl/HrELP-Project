using System.ComponentModel.DataAnnotations;

namespace HrELP.Domain.Entities.Enums
{
    public enum Currency
    {
        [Display(Name = "TRY")]
        tl,
        USD,
        Euro,
        GBP,
        CHF
    }
}