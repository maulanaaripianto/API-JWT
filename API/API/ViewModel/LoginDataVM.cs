using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class LoginDataVM
    {
        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Role Name")]
        public string[] RoleName { get; set; }
    }
}
