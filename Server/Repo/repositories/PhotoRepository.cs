using Server.Date;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
