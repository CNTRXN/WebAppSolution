using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataContext.Models
{
    [Table("Права")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
