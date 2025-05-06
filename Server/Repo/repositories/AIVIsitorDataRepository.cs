using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Date;
using Server.Models.DTOs.AIVIsitorDataDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;
using Server.Repo.repositories.service;

namespace Server.Repo.repositories
{
    public class AIVIsitorDataRepository : GenericRepository<AIVIsitorData>, IAIVIsitorDataRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public AIVIsitorDataRepository(ApplicationDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(CreateAiVIsitorDataDto createAiV)
        {
            if (createAiV == null)
            {
                return false;
            }

            var Aivistor = _mapper.Map<AIVIsitorData>(createAiV);
            Aivistor.TimeStamp = DateTime.UtcNow;
            _context.AIVIsitorDatas.Add(Aivistor);
            await _context.SaveChangesAsync();

            try
            {
                // Fix: Extract the Files property from createAiV.Photos (IFormCollection) to pass IFormFileCollection
                var imagePath = await _imageManagementService
                   .AddImagesAsync(createAiV.Photos, createAiV.Name);

                if (imagePath == null || !imagePath.Any())
                    throw new Exception("Image upload failed");

                var photo = imagePath.Select(x => new Photo
                {
                    ImageName = x,
                    AIVIsitorDataId = Aivistor.Id,
                }).ToList();

                await _context.Photos.AddRangeAsync(photo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                throw new Exception("Error uploading image", ex);
            }
            return true;
        }

        public async Task<AiVIsitorDataDTO> GetByIdAsync(int id)
        {
          var Aivistor =await _context.AIVIsitorDatas
                .Include(x => x.PhotoPath)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Aivistor == null)
            {
                throw new Exception("AIVIsitorData not found");
            }
            var AivistorDto = _mapper.Map<AiVIsitorDataDTO>(Aivistor);
            return AivistorDto;
        }

      public  async Task<IEnumerable<AiVIsitorDataDTO>>  GetAllAsync()
        {
             var Aivistor =await _context.AIVIsitorDatas
                .Include(x => x.PhotoPath)
                .AsNoTracking()
                .ToListAsync();
            var AivistorDto = _mapper.Map<IEnumerable<AiVIsitorDataDTO>>(Aivistor);
            return AivistorDto;
        }
    }
}