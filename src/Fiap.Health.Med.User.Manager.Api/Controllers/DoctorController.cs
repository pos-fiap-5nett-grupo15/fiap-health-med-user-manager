using Fiap.Health.Med.User.Manager.Application.DTOs;
using Fiap.Health.Med.User.Manager.Application.Services;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.User.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;

        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorResponseDto>> Get() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> Get(int id)
        {
            var doctor = await _service.GetByIdAsync(id);

            if (doctor == null) return NotFound();

            return doctor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Doctor doctor)
        {
            var id = await _service.AddAsync(doctor);

            return CreatedAtAction(nameof(Get), new { id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Doctor doctor)
        {
            if (id != doctor.Id) return BadRequest();

            await _service.UpdateAsync(doctor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
