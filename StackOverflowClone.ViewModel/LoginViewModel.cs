using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
