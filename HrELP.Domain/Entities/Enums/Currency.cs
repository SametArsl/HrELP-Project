using System.ComponentModel.DataAnnotations;

namespace HrELP.Domain.Entities.Enums
{
    public enum Currency
    {
        [Display(Name = "TRY")]
        TL,
        USD,
        EUR,
        GBP,
        CHF
    }
}