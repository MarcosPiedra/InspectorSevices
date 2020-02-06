using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using InspectorServices.WebAPI.DTOs;
using InspectorServices.Domain;
using InspectorServices.Domain.Models;

namespace InspectorServices.WebAPI.Validation
{
    public class InspectionValidator : AbstractValidator<InspectionRequest>
    {
        private readonly IRepository<Inspection> repository;

        public InspectionValidator(IRepository<Inspection> repository)
        {
            this.repository = repository;

            RuleFor(m => m.Customer).NotEmpty().WithMessage("Customer name is required");
            RuleFor(m => m.Address).NotEmpty().WithMessage("Address name is required");
        }
    }
}
