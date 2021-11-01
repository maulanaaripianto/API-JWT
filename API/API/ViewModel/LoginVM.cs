using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        public string password { get; set; }
    }
}
