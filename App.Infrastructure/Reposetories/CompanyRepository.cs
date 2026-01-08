using App.Domain.Entities.Basics;
using App.Domain.IReposetories;
using App.Infrastructure.Data;

namespace App.Infrastructure.Reposetories
{
    public class CompanyRepository: BaseRepository<Clinic>, IClinicRepository
    {
        public CompanyRepository(AppDbContext dbContext):base(dbContext)
        {
                
        }
    }
}
