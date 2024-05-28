using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Файлы заявки")]
    public class RequestFile
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int RequestId { get; set; }
        [Required]
        public string FilePath { get; set; }
        
        [ForeignKey("RequestId")]
        public Request Request { get; set; }
    }
}
