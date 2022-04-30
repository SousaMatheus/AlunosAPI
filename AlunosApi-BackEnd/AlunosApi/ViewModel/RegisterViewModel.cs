using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; private set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirma senha")]
        [Compare("Password", ErrorMessage = "Senhas não conferem")]
        public string ConfirmPassword { get; private set; }

    }
}
