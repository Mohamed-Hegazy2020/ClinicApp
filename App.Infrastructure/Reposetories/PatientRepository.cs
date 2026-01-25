using App.Domain.Entities.Basics;
using App.Domain.IReposetories;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Reposetories
{
	public class PatientRepository : BaseRepository<Patient>, IPatientRepository
	{
		protected readonly AppDbContext _dbContext;
		public PatientRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;

		}
		public async Task<int> GetNewCodeAsync()
		{
			int rows = await _dbContext.Patient.OrderBy(o=>o.Id).Select(x=>x.Code).LastOrDefaultAsync()+1;
			return rows;
		}

	}
}
