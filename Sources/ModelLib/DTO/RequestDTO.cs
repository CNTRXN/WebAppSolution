using ModelLib.Model;
using ModelLib.Convert.Attributes;
using ModelLib.Convert.Table;
using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    public class RequestDTO : IMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        [SelectedValue(true), Required]
        public RequestType RequestType { get; set; }
        [SelectedValue(true), Required]
        public RequestStatus RequestStatus { get; set; }
        [SelectedValue(true)]
        public UserDTO? FromUser { get; set; }
        [SelectedValue(true), Required]
        public CabinetDTO Cabinet { get; set; }
        public List<string> Images { get; set; }
    }
}
