using AppointmentBookingApp.Application.Appointments.Commands;
using AppointmentBookingApp.Application.Appointments.Queries;
using AppointmentBookingApp.Application.Professionals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Server.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AddEditAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update-appointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AddEditAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("get-all-appointments")]
        public async Task<IActionResult> GetAllAppointments(string userId, string userRole)
        {
            var query = new GetAllAppointmentsQuery(userId, userRole);
            var getAllAppointments = await _mediator.Send(query);
            return Ok(getAllAppointments);
        }

        [HttpGet("get-appointment-by-id/{id}")]
        public async Task<IActionResult> GetAllAppointments(int id)
        {
            var query = new GetAppointmentByIdQuery(id);
            var appointmnet = await _mediator.Send(query);
            return Ok(appointmnet);
        }
    }
}
