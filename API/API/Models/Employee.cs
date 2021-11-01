﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Employee")]
    [Index(nameof(Employee.Phone), IsUnique = true)]
    [Index(nameof(Employee.Email), IsUnique = true)]
    public class Employee
    {
      
        [Key]
        public string NIK { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { set; get; }
        public string Email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account StatAccount   { get; set; }
        
    }
    public enum Gender
    {
        Male,
        Female
    }
}
