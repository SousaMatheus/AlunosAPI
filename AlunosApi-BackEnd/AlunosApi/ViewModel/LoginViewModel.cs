using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; private set; }
        [Required(ErrorMessage ="A senha é obrigatória")]
        [StringLength(25, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo " +
            "{1} caracteres.", MinimumLength =10)]
        [DataType(DataType.Password)]
        public string Password { get; private set; }
    }
}
