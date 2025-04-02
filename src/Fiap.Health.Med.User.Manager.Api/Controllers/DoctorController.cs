using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorById;
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

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<GetDoctorOutput>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            if (await _service.GetAllAsync() is var result && !result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetDoctorOutput))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GetDoctorOutput>> GetOneAsync(int id)
        {
            if (await _service.GetByIdAsync(id) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{concilUf}/{concilNumber}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserSearchResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserSearchResponseDto>> GetOneByConcilAsync(string concilUf, int concilNumber)
        {
            if (await _service.GetByConcilAsync(concilUf, concilNumber) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDoctorInput doctor)
        {
            if (await _service.AddAsync(doctor) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateDoctorInput doctor)
        {
            if (id == default)
                return BadRequest();

            if (await _service.UpdateAsync(id, doctor) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.DeleteAsync(id) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
