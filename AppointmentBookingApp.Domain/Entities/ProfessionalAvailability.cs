using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Domain.Entities
{
    public class ProfessionalAvailability
    {
        public int Id { get; set; } // Primary Key
        public int ProfessionalId { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }

        public Professional Professional { get; set; }
    }

}
