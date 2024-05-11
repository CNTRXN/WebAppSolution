namespace WebApp.Models.DTO
{
    public class NewCabinetDTO
    {
        public int Num { get; set; }//Номер кабиента
        public int PlanNum { get; set; }//Номер кабинета по плану
        public int ResponsiblePersonId { get; set; }//Заведующий кабинетом
        //public int Group { get; set; }//
        public int Floor { get; set; }//Этаж
        public double Height { get; set; }//Высота
        public double Length { get; set; }//Длина
        public double Width { get; set; }//Ширина
    }
}
