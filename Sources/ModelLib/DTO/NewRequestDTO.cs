using Microsoft.AspNetCore.Http;

namespace ModelLib.DTO
{
    public class NewRequestDTO
    {
        public int CabinetId { get; set; }
        public int? UserId { get; set; }
        public List<int> EquipmentsIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
