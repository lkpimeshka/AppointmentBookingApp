using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Appointments.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; } = 0;
        public string UserId { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ProfessionalAvailabilityId { get; set; }
        public string Status { get; set; }
    }
}
