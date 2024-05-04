using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Фотографии заявки")]
    public class RequestImages
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string ImagePath { get; set; }
        
        [ForeignKey("RequestId")]
        public Request Request { get; set; }
    }
}
