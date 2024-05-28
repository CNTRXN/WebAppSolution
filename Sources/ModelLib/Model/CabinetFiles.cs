using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Файлы кабинета")]
    public class CabinetFiles
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int CabinetId { get; set; }
        [Required]
        public string FilePath { get; set; }
        public int? FileAuthor { get; set; }

        [ForeignKey("CabinetId")]
        public Cabinet Cabinet { get; set; }
        [ForeignKey("FileAuthor")]
        public User User { get; set; }
    }
}
