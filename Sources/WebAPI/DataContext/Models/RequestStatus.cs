using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Статус заявки")]
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
