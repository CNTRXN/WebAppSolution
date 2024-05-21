using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Статус заявки")]
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
