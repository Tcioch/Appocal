using System.ComponentModel.DataAnnotations;

namespace Appocal.ViewModels
{
    public class IndividualRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        [StringLength(35, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków.", MinimumLength = 5)]
        public string UserName { get; set; }

        public IndividualRegisterViewModel()
        {
            AccountType = "Individual";
        }
    }
}
