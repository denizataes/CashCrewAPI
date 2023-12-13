using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;
using Entities.Models;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
	[Route("api/vacation")]
    public class VacationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public VacationController(IServiceManager manager)
        {
            _manager = manager;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("CreateVacation")]
        [Authorize]
        public async Task<IActionResult> CreateVacationAsync([FromBody] VacationDto vacationDto)
        {
            var vacation = await _manager.VacationService.CreateVacationAsync(vacationDto);
            return StatusCode(201, vacation); // CreatedAtRoute()
        }

        [HttpGet("GetVacationsByUserId")]
        [Authorize]
        public async Task<List<VacationDto>> GetVacationsByUserIdAsync([FromQuery(Name = "userId")] int userId)
        {
            return await _manager.VacationService.GetVacationsByUserIdAsync(userId);
        }

        [HttpPost("JoinVacation")]
        [Authorize]
        public async Task<ResultModel<bool>> JoinVacationAsync([FromBody] JoinVacationDto joinVacationDto)
        {
            return await _manager.VacationService.JoinVacationAsync(joinVacationDto);
        }

        [HttpGet("SearchVacation")]
        [Authorize]
        public async Task<IActionResult> SearchVacation([FromQuery] SearchParameters searchParameters)
        {
            var pagedResult = await _manager
                .VacationService
                .GetAllVacationAsync(searchParameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.vacations);
        }



    }
}

