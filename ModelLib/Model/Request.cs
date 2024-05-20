using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Заявки")]
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int RequestTypeId { get; set; }
        public int RequestStatusId { get; set; }
        public int? FromId { get; set; }
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
