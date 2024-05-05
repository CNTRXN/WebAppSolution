using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Файлы кабинета")]
    public class CabinetFiles
    {
        [Key]
        public int Id { get; set; }
        public int CabinetId { get; set; }
        public string FilePath { get; set; }
        public int? FileAuthor { get; set; }

        [ForeignKey("CabinetId")]
        public Cabinet Cabinet { get; set; }
        [ForeignKey("FileAuthor")]
        public User User { get; set; }
    }
}
