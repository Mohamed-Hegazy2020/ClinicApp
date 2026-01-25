using App.Domain.Entities.Basics;

namespace App.Domain.IReposetories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
		Task<int> GetNewCodeAsync();

		}
}
