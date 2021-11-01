using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Education")]
    public class Education
    {
        public int Id{ get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }
        public virtual University University { get; set; }
    }
}
