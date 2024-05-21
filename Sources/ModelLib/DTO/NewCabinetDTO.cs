using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.DTO
{
    public class NewCabinetDTO
    {
        public int Num { get; set; }//Номер кабиента
        public int PlanNum { get; set; }//Номер кабинета по плану
        public int? ResponsiblePersonId { get; set; }//Заведующий кабинетом
        public int Floor { get; set; }//Этаж
        public double Height { get; set; }//Высота
        public double Length { get; set; }//Длина
        public double Width { get; set; }//Ширина
    }
}
