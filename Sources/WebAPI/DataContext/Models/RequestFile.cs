using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Файлы заявки")]
    public class RequestFile
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string FilePath { get; set; }
        
        [ForeignKey("RequestId")]
        public Request Request { get; set; }
    }
}
