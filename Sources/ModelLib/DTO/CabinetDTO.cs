using ModelLib.Convert.Table;
using ModelLib.Convert.Attributes;

namespace ModelLib.DTO
{
    [AlternativeName("Кабинеты")]
    public class CabinetDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude)]
        public int Id { get; set; }

        [AlternativeName("Номер кабинета")]
        public int Num { get; set; }

        [AlternativeName("Номер кабинета по плану")]
        public int PlanNum { get; set; }

        [AlternativeName("Ответственное лицо")]
        public UserDTO? ResponsiblePerson { get; set; }

        [AlternativeName("Этаж")]
        public int Floor { get; set; }

        [AlternativeName("Высота"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Height { get; set; }

        [AlternativeName("Длинна"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Length { get; set; }

        [AlternativeName("Ширина"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double Width { get; set; }

        [AlternativeName("Площадь потолка и пола"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareFloor => Width * Length;
        
        [AlternativeName("Площадь стен 1"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareWall_1 => Length * Height;
        
        [AlternativeName("Площадь стен 2"), InclusionInHeader(HeaderInclusion.NotInclude)]
        public double SquareWall_2 => Width * Height;
    }
}
