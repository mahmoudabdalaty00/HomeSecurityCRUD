namespace Server.Models.Entities
{
    public class AIVIsitorData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public virtual List<Photo> PhotoPath  { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
