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
    [Table("Tb_M_Roles")]
    public class Roles
    {
        [Key]
        public int Id { set; get; }
        public string Role_Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
