using AutoMapper;
using Server.Exceptions;
using Server.Models.DTOs;
using Server.Models.DTOs.HistoryDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class HistoryRepository : IHistoriesRepository
    {
        private readonly IGenericRepository<History> _repository;
        private readonly IMapper _mapper;
        public HistoryRepository(IGenericRepository<History> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddAsync(CreateHistoryDto historyDto)
        {
            var history = _mapper.Map<History>(historyDto);
            int result = await _repository.AddAsync(history);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House added successfully" : "Failed to add house"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _repository.DeleteAsync(id);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House deleted successfully" : "Failed to delete house"
            };
        }

        public async Task<IEnumerable<GetHistoryDto>> GetAllAsync()
        {
            var histories = await _repository.GetAllAsync();
            if (histories == null || !histories.Any())
            {
                return Enumerable.Empty<GetHistoryDto>();
            }
            // Map the list of House entities to a list of GetHouseDTOs
            var historiesDTOs = _mapper.Map<IEnumerable<GetHistoryDto>>(histories);

            return historiesDTOs;
        }

        public async Task<GetHistoryDto> GetByIdAsync(Guid id)
        {
            var history = await _repository.GetByIdAsync(id);
            if (history == null)
            {
                throw new NotFoundException("House not found");
            }
            // Map the House entity to a GetHouseDTO
            var historyDTO = _mapper.Map<GetHistoryDto>(history);

            return historyDTO;
        }
    }
}
