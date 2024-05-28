using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Права доступа")]
    public class Permission
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
