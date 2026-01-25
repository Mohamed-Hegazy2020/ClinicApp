using App.Domain.Entities.Basics;

namespace App.Application.IServices
{
    public interface IPatientService
    {
		public Task<Patient> GetPatientByID(int id);
		public Task<int> AddPatientAsync(Patient Patient);
		public Task<int> UpdatePatientAsync(Patient Patient);
		public Task<int> DeletePatientAsync(Patient Patient);
		public IQueryable<Patient> GetAllPatientsAsNoTracking();
		public IQueryable<Patient> GetAllPatientsAsTracking();
		Task<int> GetNewCodeAsync();

	}
}
