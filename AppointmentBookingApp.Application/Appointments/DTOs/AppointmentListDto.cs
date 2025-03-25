using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Appointments.DTOs
{
    public class AppointmentListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProfessionalId { get; set; }
        public string ProfessionalName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ProfessionalAvailabilityId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
    }
}
