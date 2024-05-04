using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Default
{
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public string ImageUrl { get; set; }
        public int FromId { get; set; }
    }
}
