using WebApp.Attributes;

namespace WebApp.Models.EquipData
{
    [AlternativeName("Кабинеты")]
    public class Cabinet : IMetaData
    {
        [AlternativeName("ИД")]
        public int Id { get; set; }
        [AlternativeName("Номер кабинета")]
        public int Num { get; set; }
        [AlternativeName("Номер кабинета по плану")]
        public int PlanNum { get; set; }
        [AlternativeName("Ответственное лицо")]
        public int ResponsiblePerson { get; set; }
        [AlternativeName("Группа")]
        public int Group { get; set; }
        [AlternativeName("Этаж")]
        public int Floor { get; set; }
        [AlternativeName("Высота")]
        public double Height { get; set; }
        [AlternativeName("Длинна")]
        public double Length { get; set; }
        [AlternativeName("Ширина")]
        public double Width { get; set; }
        [AlternativeName("Площадь потолка и пола")]
        public double SquareFloor => Width * Length;
        [AlternativeName("Площадь стен 1")]
        public double SquareWall_1 => Length * Height;
        [AlternativeName("Площадь стен 2")]
        public double SquareWall_2 => Width * Height;
    }
}
