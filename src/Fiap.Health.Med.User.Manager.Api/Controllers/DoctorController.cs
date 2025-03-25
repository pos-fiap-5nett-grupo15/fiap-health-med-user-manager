using Fiap.Health.Med.User.Manager.Application.DTOs;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.User.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Doctor doctor)
        {

            var result = await _service.AddAsync(doctor);
            if (!result.Success)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Data }, null);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Doctor doctor)
        {
            if (id != doctor.Id) return BadRequest();

            var result = await _service.UpdateAsync(doctor);

            if (!result.Success)
                return BadRequest(result.Errors);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
