using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Appointments
{
    public interface IAppointmentsServices
    {
        public Task<List<Appointment>> GetList();
        public Task<Appointment> GetOne(int idAppointment);
        public Task<bool> Add(Appointment appointment);
        public Task<bool> Update(Appointment appointmentUpdated);
        public Task<bool> Remove(int idAppointment);
        public Task<List<Appointment>> Filter(string name);
        public List<Appointment> GetListByDay();
        public Task<List<AppointmentEvents>> GetEvents();
        public Task<bool> SaveEvent(Appointment appointment);
    }
}
