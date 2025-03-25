using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentBookingApp.Application.Professionals.Queries;
using AppointmentBookingApp.Application.Professionals.Commands;

namespace AppointmentBookingApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfessionalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-specialization/{specialization}")]
        public async Task<IActionResult> GetBySpecialization(string specialization)
        {
            var query = new GetProfessionalsBySpecializationQuery(specialization);
            var professionals = await _mediator.Send(query);
            return Ok(professionals);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProfessionalByIdQuery(id);
            var professional = await _mediator.Send(query);
            return Ok(professional);
        }

        [HttpGet("get-by-userid/{userid}")]
        public async Task<IActionResult> GetByUserId(string userid)
        {
            var query = new GetProfessionalByUserIdQuery(userid);
            var professional = await _mediator.Send(query);
            return Ok(professional);
        }

        [HttpPost("create-timeslot")]
        public async Task<IActionResult> CreateTimeSlot([FromBody] AddEditAvailabilityCommand command)
        {
            var result = await _mediator.Send(command); 

            if (result.IsSuccess) 
                return Ok(result); 

            return BadRequest(result);
        }

        [HttpPut("update-timeslot")]
        public async Task<IActionResult> UpdateTimeSlot([FromBody] AddEditAvailabilityCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess) 
                return Ok(result); 

            return BadRequest(result);
        }

        [HttpGet("get-available-timeslots-by-professional-id/{id}")]
        public async Task<IActionResult> GetAvailableTimeSlotsByProfessionalId(int id)
        {
            var query = new GetAvailableTimeslotByIdQuery(id);
            var professionals = await _mediator.Send(query);
            return Ok(professionals);
        }

        [HttpGet("get-all-professional-availability-by-professional-id/{professionalId}")]
        public async Task<IActionResult> GetAllProfessionalAvailabilityByProfessional(int professionalId)
        {
            var query = new GetAllProfessionalAvailabilityQuery(professionalId);
            var availableTimeSlots = await _mediator.Send(query);
            return Ok(availableTimeSlots);
        }
    }
}
