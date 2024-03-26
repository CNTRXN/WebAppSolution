using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Фотографии кабинета")]
    public class CabPhoto
    {
        [Key]
        public int Id { get; set; }
        public int CabId { get; set; }
        public string ImagePath { get; set; }
        public int ImageAuthor { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }


        [ForeignKey("CabId")]
        public Cabinet Cabinet { get; set; }
        [ForeignKey("ImageAuthor")]
        public User User { get; set; }
    }
}
