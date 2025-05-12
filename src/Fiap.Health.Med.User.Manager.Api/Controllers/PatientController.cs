using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.CreatePatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.GetPatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.UpdatePatient;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Health.Med.User.Manager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<GetPatientOutput>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            if (await _service.GetAllAsync() is var result && !result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetPatientOutput))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GetPatientOutput>> GetOneAsync(int id)
        {
            if (await _service.GetByIdAsync(id) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("Document/{document}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetPatientOutput))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GetPatientOutput>> GetOneByDocumentAsync(long document)
        {
            if (await _service.GetByDocumentAsync(document) is var result && !result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePatientInput patient)
        {
            if (await _service.AddAsync(patient) is var result && !result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdatePatientInput patient)
        {
            if (id == default)
                return BadRequest();

            if (await _service.UpdateAsync(id, patient) is var result && result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.DeleteAsync(id) is var result && result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
