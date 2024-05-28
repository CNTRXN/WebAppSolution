using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Статус заявки")]
    public class RequestStatus
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}
