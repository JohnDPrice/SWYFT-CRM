using SWYFT_CRM.Models;
using System.Collections.Generic;

namespace SWYFT_CRM.Repositories
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        void Delete(int id);
        List<Appointment> GetAllUserAppointments(int id);
        Appointment GetById(int id);
        void Update(Appointment appointment);
    }
}