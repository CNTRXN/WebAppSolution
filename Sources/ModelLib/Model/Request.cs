using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Заявки")]
    public class Request
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        [Required]
        public int RequestTypeId { get; set; }
        [Required]
        public int RequestStatusId { get; set; }
        public int? FromId { get; set; }
        [Required]
        public int CabId { get; set; }


        [ForeignKey("FromId")]
        public User User { get; set; }
        [ForeignKey("CabId")]
        public Cabinet Cabinet { get; set; }
        [ForeignKey("RequestTypeId")]
        public RequestType RequestType { get; set; }
        [ForeignKey("RequestStatusId")]
        public RequestStatus RequestStatus { get; set; }
    }
}
