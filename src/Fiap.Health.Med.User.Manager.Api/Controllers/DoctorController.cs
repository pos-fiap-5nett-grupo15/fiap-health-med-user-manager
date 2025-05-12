using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetById;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorsByFilters;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Health.Med.User.Manager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet("filters")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetDoctorsByFiltersOutput))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByFilter([FromQuery] string? doctorName,
                                                     [FromQuery] EMedicalSpecialty? doctorSpecialty,
                                                     [FromQuery] int? doctorDoncilNumber,
                                                     [FromQuery] string? doctorCrmUf,
                                                     [FromQuery] int? currentPage,
                                                     [FromQuery] int? pageSize,
                                                     CancellationToken ct)
        {
            var result = await _service.GetByFilterAsync(doctorName, doctorSpecialty, doctorDoncilNumber, doctorCrmUf, currentPage ?? 1, pageSize ?? 10, ct);
            return StatusCode((int)result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetByIdOutput))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GetByIdOutput>> GetOneAsync(int id, CancellationToken ct)
        {
            if (await _service.GetByIdAsync(id, ct) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{concilUf}/{concilNumber}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserSearchResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserSearchResponseDto>> GetOneByConcilAsync(string concilUf, int concilNumber, CancellationToken ct)
        {
            if (await _service.GetByConcilAsync(concilUf, concilNumber, ct) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDoctorInput doctor, CancellationToken ct)
        {
            if (await _service.AddAsync(doctor, ct) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateDoctorInput doctor, CancellationToken ct)
        {
            if (id == default)
                return BadRequest();

            if (await _service.UpdateAsync(id, doctor, ct) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            if (await _service.DeleteAsync(id, ct) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
