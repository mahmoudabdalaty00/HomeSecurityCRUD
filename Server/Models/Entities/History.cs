using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }

    }

}
