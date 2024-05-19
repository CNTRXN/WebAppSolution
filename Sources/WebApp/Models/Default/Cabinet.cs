using WebApp.Attributes;

namespace WebApp.Models.EquipData
{
    public class Cabinet
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public int PlanNum { get; set; }
        public int? ResponsiblePerson { get; set; }
        //public int Group { get; set; }
        public int Floor { get; set; } 
        public double Height { get; set; }   
        public double Length { get; set; }
        public double Width { get; set; } 
        public double SquareFloor => Width * Length;
        public double SquareWall_1 => Length * Height;
        public double SquareWall_2 => Width * Height;
    }
}
