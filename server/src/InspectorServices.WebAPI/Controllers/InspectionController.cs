using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InspectorServices.Domain;
using InspectorServices.Domain.Models;
using InspectorServices.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InspectorServices.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class InspectionController : Controller
    {
        private readonly ILogger<InspectionController> logger;
        private readonly IInspectionService inspectionServices;
        private readonly IValidator<InspectionRequest> validator;
        private readonly IMapper mapper;

        public InspectionController(ILogger<InspectionController> logger,
                                    IInspectionService inspectionServices,
                                    IValidator<InspectionRequest> validator,
                                    IMapper mapper)
        {
            this.logger = logger;
            this.inspectionServices = inspectionServices;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<InspectionResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<InspectionResponse>>> GetAllInspection()
        {
            var inspectionList = await this.inspectionServices.GetAllAsync();

            if (inspectionList == null)
            {
                return Ok();
            }

            var inspectionResponseList = this.mapper.Map<List<Inspection>, List<InspectionResponse>>(inspectionList);

            logger.LogInformation($"GetAllInspection");

            return inspectionResponseList;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(InspectionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<InspectionResponse>> GetInspection(int id)
        {
            var inspection = await this.inspectionServices.FindAsync(id);

            if (inspection == null)
            {
                return NotFound();
            }

            var managerResponse = this.mapper.Map<Inspection, InspectionResponse>(inspection);

            logger.LogInformation($"GetInspection {id}");

            return managerResponse;
        }

        [HttpGet("StatusList")]
        [ProducesResponseType(typeof(List<StatusResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<StatusResponse>>> GetStatus()
        {
            var statusList = ((Status[])Enum.GetValues(typeof(Status))).ToList();

            if (statusList.Count == 0)
            {
                return NotFound();
            }

            var statusResponseList = new List<StatusResponse>();
            statusList.ForEach(s => statusResponseList.Add(new StatusResponse() { Id = (int)s, Name = s.ToString() }));

            logger.LogInformation($"GetStatus");

            return statusResponseList;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateInspection([FromBody] InspectionRequest inspectionRequest)
        {
            if (inspectionRequest == null)
            {
                return BadRequest();
            }

            var validate = validator.Validate(inspectionRequest);
            if (!validate.IsValid)
            {
                return BadRequest(validate);
            }

            var inspection = this.mapper.Map<InspectionRequest, Inspection>(inspectionRequest);

            await this.inspectionServices.AddAsync(inspection);

            logger.LogInformation($"CreateInspection {inspection.Id}");

            return CreatedAtAction(nameof(CreateInspection), new { id = inspection.Id, version = this.HttpContext.GetRequestedApiVersion().ToString() }, inspection.Id);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateInspection(int id, [FromBody] InspectionRequest inspectionRequest)
        {
            if (id < 1 || inspectionRequest == null)
            {
                return BadRequest();
            }

            var validate = validator.Validate(inspectionRequest);
            if (!validate.IsValid)
            {
                return BadRequest(validate);
            }

            var inspectionToUpdate = await this.inspectionServices.FindAsync(id);
            if (inspectionToUpdate == null)
            {
                return NotFound();
            }

            var inspection = this.mapper.Map(inspectionRequest, inspectionToUpdate);

            await this.inspectionServices.UpdateAsync(inspection);

            logger.LogInformation($"UpdateInspection {inspection.Id}");

            return Ok();
        }

    }
}