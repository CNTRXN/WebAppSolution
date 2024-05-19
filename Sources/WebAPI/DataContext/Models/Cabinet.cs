using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataContext.Models
{
    [Table("Кабинеты")]
    public class Cabinet
    {
        [Key]
        public int Id { get; set; }//Номер записи
        public int Num { get; set; }//Номер кабиента
        public int PlanNum { get; set; }//Номер кабинета по плану
        public int? ResponsiblePersonId { get; set; }//Заведующий кабинетом
        //public int Group { get; set; }//
        public int Floor { get; set; }//Этаж
        public double Height { get; set; }//Высота
        public double Length { get; set; }//Длина
        public double Width { get; set; }//Ширина
        //public double Square { get; set; }//Площадь
        public double SquareFloor => Width * Length;
        public double SquareWall_1 => Length * Height;
        public double SquareWall_2 => Width * Height;

        [ForeignKey("ResponsiblePersonId")]
        public User User { get; set; }
    }
}
