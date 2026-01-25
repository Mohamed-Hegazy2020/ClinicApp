using App.Application.IServices;
using App.Domain.Entities.Basics;
using App.Domain.IReposetories;

namespace App.Application.Services
{
	public class PatientService : IPatientService
    {
		private readonly IPatientRepository _PatientRepository;
		public PatientService(IPatientRepository PatientRepository)
		{
			_PatientRepository = PatientRepository;
		}
		public async Task<int> AddPatientAsync(Patient Patient)
		{
			int rows = await _PatientRepository.AddAsync(Patient);
			return rows;
		}

		public async Task<int> DeletePatientAsync(Patient Patient)
		{
			int rows = await _PatientRepository.DeleteAsync(Patient);
			return rows;
		}

		public IQueryable<Patient> GetAllPatientsAsNoTracking()
		{
			return _PatientRepository.GetTableAsNoTracking();
		}
		public IQueryable<Patient> GetAllPatientsAsTracking()
		{
			return _PatientRepository.GetTableAsNoTracking();
		}

		public async Task<Patient> GetPatientByID(int id)
		{
			return await _PatientRepository.GetByIdAsync(id);

		}

		public async Task<int> UpdatePatientAsync(Patient Patient)
		{
			int rows = await _PatientRepository.UpdateAsync(Patient);
			return rows;
		}
		public async Task<int> GetNewCodeAsync()
		{
			int rows = await _PatientRepository.GetNewCodeAsync();
			return rows;
		}




	}
}
