using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.DTO
{
    public class NewCabinetDTO
    {
        [Required]
        public int Num { get; set; }//Номер кабиента
        [Required]
        public int PlanNum { get; set; }//Номер кабинета по плану
        public int? ResponsiblePersonId { get; set; }//Заведующий кабинетом
        [Required]
        public int Floor { get; set; }//Этаж
        [Required]
        public double Height { get; set; }//Высота
        [Required]
        public double Length { get; set; }//Длина
        [Required]
        public double Width { get; set; }//Ширина
    }
}
