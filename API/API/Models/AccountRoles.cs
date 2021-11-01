using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_AccountRoles")]
    public class AccountRoles
    {
        [Key]
        public int Account_Id { get; set; }
        public string AccountNIK { get; set;}
        public int RolesId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
