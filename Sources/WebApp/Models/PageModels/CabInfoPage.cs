using WebApp.Models.DTO;

namespace WebApp.Models.PageModels
{
    public class CabInfoPage
    {
        public List<dynamic> Equipments { get; set; }
        public CabinetDTO Cabinet { get; set; }
        public string SearchQuery { get; set; }
        public string SelectedList { get; set; } = "Equipment";
    }
}
