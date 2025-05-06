namespace Server.Models.DTOs.AIVIsitorDataDTO
{
    public class AiVIsitorDataDTO
    {       
        public virtual List<PhotoDTO> Photos { get; set; }
    }
    public class CreateAiVIsitorDataDto
    {
        public string Name { get; set; }
        public string Classification { get; set; }
        public IFormFileCollection Photos { get; set; }

    }

}
