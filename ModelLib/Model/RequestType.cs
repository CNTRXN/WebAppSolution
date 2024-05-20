using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Тип заявки")]
    public class RequestType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
