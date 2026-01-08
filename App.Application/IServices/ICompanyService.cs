using App.Domain.Entities.Basics;

namespace App.Application.IServices
{
    public interface IClinicService
    {
        public Task<Clinic> GetClinicByID(int id);  
        public Task<int> AddClinicAsync(Clinic Clinic);
        public Task<int> UpdateClinicAsync(Clinic Clinic);
        public Task<int> DeleteClinicAsync(Clinic Clinic);
        public IQueryable<Clinic> GetAllClinicsAsNoTracking();
        public IQueryable<Clinic> GetAllClinicsAsTracking();
    }
}
