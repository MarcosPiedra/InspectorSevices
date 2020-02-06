using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using InspectorServices.Domain;
using InspectorServices.Domain.Models;
using InspectorServices.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InspectorServices.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class InspectorController : Controller
    {
        private readonly ILogger<InspectorController> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Inspector> repository;

        public InspectorController(ILogger<InspectorController> logger,
                                   IRepository<Inspector> inspectorRepository,
                                   IMapper mapper)
        {
            this.logger = logger;
            this.repository = inspectorRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<InspectorResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<InspectorResponse>>> GetAllInspector()
        {
            var inspectorList = await this.repository.Query
                                                     .OrderBy(i => i.Name)
                                                     .ToListAsync();

            if (inspectorList == null)
            {
                return Ok();
            }

            var inspectorResponseList = this.mapper.Map<List<Inspector>, List<InspectorResponse>>(inspectorList);

            logger.LogInformation($"GetAllInspector");

            return inspectorResponseList;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(InspectorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<InspectorResponse>> GetInspector(int id)
        {
            var inspector = await this.repository.Query.FirstOrDefaultAsync(i => i.Id == id);

            if (inspector == null)
            {
                return NotFound();
            }

            var inspectorResponse = this.mapper.Map<Inspector, InspectorResponse>(inspector);

            logger.LogInformation($"GetInspector {id}");

            return inspectorResponse;
        }
    }
}