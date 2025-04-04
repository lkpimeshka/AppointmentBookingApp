﻿using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Queries
{
    public class GetAllProfessionalAvailabilityQuery : IRequest<List<ProfessionalAvailabilityDto>>
    {
        public int ProfessionalId { get; set; }

        public GetAllProfessionalAvailabilityQuery(int professionalId)
        {
            ProfessionalId = professionalId;
        }
    }
}
