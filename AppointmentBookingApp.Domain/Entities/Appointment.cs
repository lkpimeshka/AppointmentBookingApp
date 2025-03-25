using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ProfessionalAvailabilityId { get; set; }
        public string Status { get; set; }
        public ApplicationUser User { get; set; }
        public Professional Professional { get; set; }
        public ProfessionalAvailability ProfessionalAvailability { get; set; }
    }
}
