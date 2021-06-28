using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWYFT_CRM.Models
{
    public class Appointment
    {
        int Id { get; set; }
        DateTime Time { get; set; }
        string Notes { get; set; }
        int LeadId { get; set; }
        Lead Lead { get; set; }
        int UserId { get; set; }
        UserProfile UserProfile { get; set; }
    }
}
