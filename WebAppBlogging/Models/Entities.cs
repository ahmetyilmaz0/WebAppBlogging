using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBlogging.Models
{
    public class Entities
    {
        [Key]
        public int EntityID { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Entity { get; set; }
        public Users User { get; set; }
    }
}
