using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Server.Models.DTOs.AIVIsitorDataDTO;
using Server.Models.Entities;

namespace Server.Repo.interfaces
{
    public interface IAIVIsitorDataRepository :IGenericRepository<AIVIsitorData>
    {
        Task<IEnumerable<AiVIsitorDataDTO>> GetAllAsync();
        Task<AiVIsitorDataDTO> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateAiVIsitorDataDto entity);
       
    }
 
}
