using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Appocal.ViewModels
{
    public abstract class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }
        [Required]
        protected string _accountType;
        public string AccountType
        {
            get
            {
                return _accountType;
            }
            set
            {
                if (AccountTypes.Any(s => s.Equals(value, StringComparison.OrdinalIgnoreCase)))
                    _accountType = value;
                else _accountType = "Individual";
            }
        }
        private List<string> AccountTypes { get; set; } = new List<string>(){ "Individual", "Business" };
    }
}
