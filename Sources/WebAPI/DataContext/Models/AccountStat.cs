using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    public class AccountStat
    {
        [Key]
        public int Id { get; set; }
        public string SignInKey {  get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationKeyDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
