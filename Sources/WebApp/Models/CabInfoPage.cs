using WebApp.Models.EquipData;
using WebApp.Models.UserData;

namespace WebApp.Models
{
    public class CabInfoPage
    {
        public List<Equipment> Equipments { get; set; }
        public Cabinet Cabinet { get; set; }
        public string SearchQuery { get; set; }
    }
}
