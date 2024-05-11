using WebApp.Attributes;
using WebApp.Settings.Converters.Table;

namespace WebApp.Models.DTO
{
    [AlternativeName("Кабинеты")]
    public class CabinetDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude)]
        public int Id { get; set; }
        [AlternativeName("Номер кабинета")]
        public int Num { get; set; }//Номер кабиента
        [AlternativeName("Номер кабинета по плану")]
        public int PlanNum { get; set; }//Номер кабинета по плану
        [AlternativeName("Ответственное лицо")]
        public UserDTO? ResponsiblePerson { get; set; }
        //[AlternativeName("Группа")]
        //public int Group { get; set; }//
        [AlternativeName("Этаж")]
        public int Floor { get; set; }//Этаж
        [AlternativeName("Высота"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Height { get; set; }//Высота
        [AlternativeName("Длинна"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Length { get; set; }//Длина
        [AlternativeName("Ширина"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Width { get; set; }//Ширина
        [AlternativeName("Площадь потолка и пола"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareFloor => Width * Length;
        [AlternativeName("Площадь стен 1"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareWall_1 => Length * Height;
        [AlternativeName("Площадь стен 2"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareWall_2 => Width * Height;
    }
}
