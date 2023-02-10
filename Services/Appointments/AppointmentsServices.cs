using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Appointments
{
    public class AppointmentsServices : IAppointmentsServices
    {
        private readonly MainDbContext _dbContext;

        public AppointmentsServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetList()
        {
            List<Appointment> list;

            try
            {
                list = await _dbContext.Appointments.Include(a => a.Animal)
                    .Include(v => v.Veterinarian)
                    .Include(p => p.Priority)
                    .Include(m => m.Motive)
                    .OrderByDescending(o => o.Schedule.Date)
                    .ThenByDescending(o => o.Schedule.TimeOfDay)
                    .ToListAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public async Task<Appointment> GetOne(int idAppointment)
        {
            Appointment appointment = null;

            try
            {
                appointment = await _dbContext.Appointments
                    .Where(ap => ap.IdAppointment == idAppointment)
                    .Include(a => a.Animal)
                    .Include(v => v.Veterinarian)
                    .Include(m => m.Motive)
                    .Include(p => p.Priority)
                    .FirstOrDefaultAsync();

            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return appointment;
        }

        public async Task<bool> Add(Appointment appointment)
        {
            try
            {
                if(await _dbContext.Appointments
                    .AnyAsync(x => x.Schedule == appointment.Schedule &&
                        x.IdVeterinarian == appointment.IdVeterinarian))
                {
                    return false;
                }
                else
                {
                    await _dbContext.Appointments.AddAsync(appointment);
                    await _dbContext.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Appointment appointmentUpdated)
        {
            try
            {
                if (await _dbContext.Appointments
                    .AnyAsync(x => x.Schedule == appointmentUpdated.Schedule &&
                         x.IdVeterinarian == appointmentUpdated.IdVeterinarian &&
                         x.IdAppointment != appointmentUpdated.IdAppointment))
                {
                    return false;
                }
                else
                {
                    _dbContext.Appointments.Update(appointmentUpdated);
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int idAppointment)
        {
            var appointment = await GetOne(idAppointment);

            if(appointment == null)
            {
                return false;
            }

            try
            {
                _dbContext.Appointments.Remove(appointment);
                await _dbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<List<Appointment>> Filter(string name)
        {
            List<Appointment> appointments = null;

            try
            {
                if(name == null)
                {
                    appointments = await _dbContext.Appointments
                        .Include(a => a.Animal)
                        .Include(v => v.Veterinarian)
                        .Include(m => m.Motive)
                        .ToListAsync<Appointment>();
                }
                else
                {
                    appointments = await _dbContext.Appointments
                        .Where(a => a.Animal.Name.ToLower().Contains(name.ToLower()) || a.Veterinarian.Name.ToLower().Contains(name.ToLower()))
                        .Include(a => a.Animal)
                        .Include(v => v.Veterinarian)
                        .Include(m => m.Motive)
                        .ToListAsync<Appointment>();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return appointments;
        }

        public List<Appointment> GetListByDay()
        {
            List<Appointment> list;

            var dateTime = DateTime.Today;

            try
            {
                list =  _dbContext.Appointments.Where(d => d.Schedule.Date.ToString() == dateTime.Date.ToString("yyyy-MM-dd"))
                    .Include(a => a.Animal)
                    .Include(v => v.Veterinarian)
                    .Include(p => p.Priority)
                    .OrderByDescending(s => s.Schedule.Date)
                    .ThenBy(s => s.Schedule.TimeOfDay)
                    .ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public async Task<List<AppointmentEvents>> GetEvents()
        {
            var list = await GetList();

            List<AppointmentEvents> appointEvents = new();

            foreach (var item in list.ToList())
            {
                appointEvents.Add(new AppointmentEvents
                {
                    Id = item.IdAppointment,
                    Title = item.Veterinarian.Name,
                    Description = item.Motive.Description,
                    Start = item.Schedule.ToString("yyyy-MM-dd HH:mm"),
                    AllDay = false
                });
            }

            return appointEvents;
        }

        public async Task<bool> SaveEvent(Appointment appointment)
        {
            Appointment appointmentDB = null;

            try
            {
                if (appointment.IdAppointment > 0)
                {
                    appointmentDB = await _dbContext.Appointments.Where(a => a.IdAppointment == appointment.IdAppointment).FirstOrDefaultAsync();

                    // Update do appointment/event
                    if (appointmentDB != null)
                    {
                        appointmentDB.IdAppointment = appointment.IdAppointment;
                        appointmentDB.Schedule = appointment.Schedule;
                        appointmentDB.Observations = appointment.Observations;
                        appointmentDB.Price = appointment.Price;
                        appointmentDB.IdAnimal = appointment.IdAnimal;
                        appointmentDB.IdVeterinarian = appointment.IdVeterinarian;
                        appointmentDB.IdMotive = appointment.IdMotive;
                        appointmentDB.IdPriority = appointment.IdPriority;
                    }

                    if (await _dbContext.Appointments
                           .AnyAsync(x => x.Schedule == appointment.Schedule &&
                           x.IdVeterinarian == appointment.IdVeterinarian))
                    {
                        return false;
                    }
                }
                else
                {
                    await _dbContext.Appointments.AddAsync(appointment);
                    await _dbContext.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }


    }
}
