using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Кабинеты")]
    public class Cabinet
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int Num { get; set; }
        [Required]
        public int PlanNum { get; set; }
        public int? ResponsiblePersonId { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Width { get; set; }
        public double SquareFloor => Width * Length;
        public double SquareWall_1 => Length * Height;
        public double SquareWall_2 => Width * Height;

        [ForeignKey("ResponsiblePersonId")]
        public User User { get; set; }
    }
}
