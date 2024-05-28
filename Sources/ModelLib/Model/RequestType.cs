using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Тип заявки")]
    public class RequestType
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}
