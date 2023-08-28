using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Extensions;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.ViewModels;

namespace MyDoctorAppointment.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(string appSettinsgs, ISerializationService serializationService)
        {
            _doctorRepository = new DoctorRepository(appSettinsgs, serializationService);
        }

        public Doctor Create(Doctor doctor)
        {
            return _doctorRepository.Create(doctor);
        }

        public bool Delete(int id)
        {
            return _doctorRepository.Delete(id);
        }

        public Doctor? Get(int id)
        {
            return _doctorRepository.GetById(id);
        }

        public IEnumerable<DoctorViewModel> GetAll()
        {
            var doctors = _doctorRepository.GetAll();
            var doctorViwModels = doctors.Select(x => x.ConvertTo());
            return doctorViwModels;
        }

        // переделать остальное по аналогии
        public Doctor Update(int id, Doctor doctor)
        {
            return _doctorRepository.Update(id, doctor);
        }
    }
}
