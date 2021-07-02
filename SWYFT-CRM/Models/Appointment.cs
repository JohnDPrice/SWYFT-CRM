using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWYFT_CRM.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Notes { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? AllDay { get; set; }
        public int LeadId { get; set; }
        public Lead Lead { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
