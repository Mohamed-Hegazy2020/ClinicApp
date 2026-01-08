using App.Domain.Entities.Basics;
using App.Domain.IReposetories;
using App.Application.IServices;

namespace App.Application.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _ClinicRepository;
        public ClinicService(IClinicRepository ClinicRepository)
        {
            _ClinicRepository = ClinicRepository;
        }
        public async Task<int> AddClinicAsync(Clinic Clinic)
        {
           int rows= await _ClinicRepository.AddAsync(Clinic);
            return rows;
        }

        public async Task<int> DeleteClinicAsync(Clinic Clinic)
        {
            int rows = await _ClinicRepository.DeleteAsync(Clinic);
            return rows;
        }

        public IQueryable<Clinic> GetAllClinicsAsNoTracking()
        {
            return _ClinicRepository.GetTableAsNoTracking();
        } 
        public IQueryable<Clinic> GetAllClinicsAsTracking()
        {
            return _ClinicRepository.GetTableAsNoTracking();
        }

        public async Task<Clinic> GetClinicByID(int id)
        {
            return await _ClinicRepository.GetByIdAsync(id);
            
        }

        public async Task<int> UpdateClinicAsync(Clinic Clinic)
        {
            int rows = await _ClinicRepository.UpdateAsync(Clinic);
            return rows;
        }
    }
}
