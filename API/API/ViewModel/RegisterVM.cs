using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter your NIK")]
        [RegularExpression(@"^.{11,}([0-9]+)$", ErrorMessage = "Minimum 12 characters required and Please enter valid Number")]
        public string NIK { get; set; }
        [Required(ErrorMessage = "Please enter your First Name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Please enter your Phone Number")]
        [RegularExpression(@"^.{10,}([0-9]+)$", ErrorMessage = "Minimum 11 characters required and Please enter valid Number")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your Birth Date")]
        public DateTime birthDate { get; set; }
        [Required(ErrorMessage = "Please enter your Salary")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int salary { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Please enter your Degree")]
        public string degree { get; set; }
        [Required(ErrorMessage = "Please enter your GPA")]
        public string gpa { get; set; }
        public int university_Id { get; set;}
        public int roles_id { get; set; }
        public Gender Gender { get; set; }
    }
}
