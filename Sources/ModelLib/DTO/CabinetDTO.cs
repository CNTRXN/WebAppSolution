using ModelLib.Convert.Table;
using ModelLib.Convert.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    [AlternativeName("Кабинеты")]
    public class CabinetDTO : IMetaData
    {
        [Required]
        [InclusionInForm(PropertyInclusion.NotInclude)]
        [InclusionInHeader(PropertyInclusion.NotInclude)]
        public int Id { get; set; }

        [AlternativeName("Номер кабинета"), Required]
        public int Num { get; set; }

        [AlternativeName("Номер кабинета по плану"), Required]
        public int PlanNum { get; set; }

        [AlternativeName("Ответственное лицо"), ShowPermission("Master, Admin"), SelectedValue(true)]
        public UserDTO? ResponsiblePerson { get; set; }

        [AlternativeName("Этаж"), Required]
        public int Floor { get; set; }

        [AlternativeName("Высота"), InclusionInHeader(PropertyInclusion.NotInclude), Required]
        public double Height { get; set; }

        [AlternativeName("Длинна"), InclusionInHeader(PropertyInclusion.NotInclude), Required]
        public double Length { get; set; }

        [AlternativeName("Ширина"), InclusionInHeader(PropertyInclusion.NotInclude), Required]
        public double Width { get; set; }

        [AlternativeName("Площадь потолка и пола")]
        [InclusionInForm(PropertyInclusion.NotInclude)]
        [InclusionInHeader(PropertyInclusion.NotInclude)]
        public double SquareFloor => Width * Length;
        
        [AlternativeName("Площадь стен 1")]
        [InclusionInForm(PropertyInclusion.NotInclude)]
        [InclusionInHeader(PropertyInclusion.NotInclude)]
        public double SquareWall_1 => Length * Height;
        
        [AlternativeName("Площадь стен 2")]
        [InclusionInForm(PropertyInclusion.NotInclude)]
        [InclusionInHeader(PropertyInclusion.NotInclude)]
        public double SquareWall_2 => Width * Height;
    }
}
