namespace WebAPI.DataContext.DTO
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int RequestTypeId { get; set; }
        public int RequestStatusId { get; set; }
        public UserDTO? FromUser { get; set; }
        public CabinetDTO Cabinet { get; set; }
        public List<string> Images { get; set; }
    }
}
