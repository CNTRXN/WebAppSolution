namespace WebAPI.DataContext.DTO
{
    public class CabinetDTO
    {
        public int Id { get; set; }
        public int Num { get; set; }//Номер кабиента
        public int PlanNum { get; set; }//Номер кабинета по плану
        public UserDTO? ResponsiblePerson { get; set; }
        //public int Group { get; set; }//
        public int Floor { get; set; }//Этаж
        public double Height { get; set; }//Высота
        public double Length { get; set; }//Длина
        public double Width { get; set; }//Ширина
    }
}
