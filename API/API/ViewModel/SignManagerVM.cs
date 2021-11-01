using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class SignManagerVM
    {
        [Required(ErrorMessage = "Please enter your NIK")]
        [RegularExpression(@"^.{11,}([0-9]+)$", ErrorMessage = "Minimum 12 characters required and Please enter valid Number")]
        public string NIK { get; set; }
        [Required(ErrorMessage = "Please enter your Role Id")]
        public string RoleId { get; set; }
    }
}
