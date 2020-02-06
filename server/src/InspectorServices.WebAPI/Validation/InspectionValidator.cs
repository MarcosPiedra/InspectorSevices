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
            RuleFor(m => m).Must(m => NoMoreInspectorsByDay(m)).WithMessage("One inspection by day");
            RuleFor(m => m).Must(m => InspectionInTheFuture(m)).WithMessage("Can't insert/update a inspection in the future");
        }
        public bool NoMoreInspectorsByDay(InspectionRequest inspectionRequest)
        {
            var q = this.repository.Query.Where(i => i.InspectorId == inspectionRequest.InspectorId);

            if (q.Count() == 0)
                return true;

            // SQLite dont use DateTime, will parse the date time string of the machine
            return q.Count(i => Convert.ToDateTime(i.Date).ToString("ddMMyyyy") == Convert.ToDateTime(inspectionRequest.Date).ToString("ddMMyyyy")) == 0;
        }

        public bool InspectionInTheFuture(InspectionRequest inspectionRequest)
        {
            return Convert.ToDateTime(inspectionRequest.Date) < DateTime.Now;
        }
    }
}
