using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Права доступа")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
