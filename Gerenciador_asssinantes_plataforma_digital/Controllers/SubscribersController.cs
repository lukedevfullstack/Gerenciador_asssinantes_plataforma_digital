using Gerenciador_asssinantes_plataforma_digital.Application.DTOs;
using Gerenciador_asssinantes_plataforma_digital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_asssinantes_plataforma_digital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _service;
        public SubscribersController(ISubscriberService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(SubscriberRequest req) => Ok(await _service.CreateAsync(req));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllActiveAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdActiveAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SubscriberRequest req)
        {
            await _service.UpdateAsync(id, req);
            return NoContent();
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _service.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}