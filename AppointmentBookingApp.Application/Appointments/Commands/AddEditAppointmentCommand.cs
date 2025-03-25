using AppointmentBookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AppointmentBookingApp.Application.Appointments;
using AppointmentBookingApp.Application.Appointments.DTOs;
using AppointmentBookingApp.Application.Professionals.DTOs;
using AppointmentBookingApp.Application.Professionals;

namespace AppointmentBookingApp.Application.Appointments.Commands
{
    public class AddEditAppointmentCommand : IRequest<AppointmentResponseDto>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ProfessionalAvailabilityId { get; set; }
        public string Status { get; set; }
    }

    public class CreateAppointmentHandler : IRequestHandler<AddEditAppointmentCommand, AppointmentResponseDto>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IProfessionalService _professionalService;

        public CreateAppointmentHandler(IAppointmentService appointmentService, IProfessionalService professionalService)
        {
            _appointmentService = appointmentService;
            _professionalService = professionalService;
        }

        public async Task<AppointmentResponseDto> Handle(AddEditAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new AppointmentResponseDto();

            try
            {
                if (request.Id == 0) 
                {
                    var appointment = new Appointment
                    {
                        UserId = request.UserId,
                        ProfessionalId = request.ProfessionalId,
                        AppointmentDate = request.AppointmentDate,
                        ProfessionalAvailabilityId = request.ProfessionalAvailabilityId,
                        Status = request.Status
                    };

                    bool isCreated = await _appointmentService.CreateAppointment(appointment);

                    if (isCreated)
                    {
                        var availability = await _professionalService.GetAvailableTimeslotByIdAsync(appointment.ProfessionalAvailabilityId);
                        if (availability != null)
                        {
                            availability.IsBooked = true;
                            await _professionalService.UpdateTimeSlot(availability);
                        }
                    }

                    response.IsSuccess = isCreated;
                    response.Message = isCreated ? "Appointment created successfully" : "Failed to create appointment";
                   
                }
                else 
                {
                    var existingAppointment = new Appointment
                    {
                        Id = request.Id,
                        UserId = request.UserId,
                        ProfessionalId = request.ProfessionalId,
                        AppointmentDate = request.AppointmentDate,
                        ProfessionalAvailabilityId = request.ProfessionalAvailabilityId,
                        Status = request.Status
                    };

                    bool isCreated = await _appointmentService.UpdateAppointment(existingAppointment);

                    response.IsSuccess = isCreated;
                    response.Message = isCreated ? "Appointment updated successfully" : "Failed to update appointment";
                    
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
