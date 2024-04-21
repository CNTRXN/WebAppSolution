using WebApp.Models.DTO;
using WebApp.Models.EquipData;
using WebApp.Models.UserData;

namespace WebApp.Models
{
    public class CabInfoPage
    {
        public List<dynamic> Equipments { get; set; }
        public CabinetDTO Cabinet { get; set; }
        public string SearchQuery { get; set; }
        public string SelectedList { get; set; } = "Equipment";
    }
}
