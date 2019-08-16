using System.ComponentModel.DataAnnotations;

namespace Appocal.ViewModels
{
    public class BusinessRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Nazwa firmy")]
        [StringLength(55, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków.", MinimumLength = 5)]
        public string BusinessName { get; set; }

    }
}
