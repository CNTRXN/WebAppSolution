using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Тип заявки")]
    public class RequestType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
