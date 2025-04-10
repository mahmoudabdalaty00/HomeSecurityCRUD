using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        [Base64String]
        [Column(TypeName = "nvarchar(max)")]
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
    }

}
