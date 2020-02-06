using AutoMapper;
using InspectorServices.Domain.Models;
using InspectorServices.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectorServices.WebAPI
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<Inspection, InspectionResponse>();
            CreateMap<InspectionRequest, Inspection>();

            CreateMap<Inspector, InspectorResponse>();
        }
    }
}
