using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataContext.Models
{
    [Table("Заявки")]
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int? ImageId { get; set; }
        public int FromId { get; set; }
        public int? CabId { get; set; }

        [ForeignKey("FromId")]
        public User User { get; set; }
        [ForeignKey("ImageId")]
        public CabPhoto CabPhoto { get; set; }
        [ForeignKey("CabId")]
        public Cabinet Cabinet { get; set; }
    }
}
