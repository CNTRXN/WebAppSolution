using ModelLib.Model;
using ModelLib.Convert.Attributes;
using ModelLib.Convert.Table;

namespace ModelLib.DTO
{
    public class RequestDTO : IMetaData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        [SelectedValue(true)]
        public RequestType RequestType { get; set; }
        [SelectedValue(true)]
        public RequestStatus RequestStatus { get; set; }
        [SelectedValue(true)]
        public UserDTO? FromUser { get; set; }
        [SelectedValue(true)]
        public CabinetDTO Cabinet { get; set; }
        public List<string> Images { get; set; }
    }
}
