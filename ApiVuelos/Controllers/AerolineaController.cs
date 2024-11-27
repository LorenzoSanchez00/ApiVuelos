using ApiVuelos.DTOs;
using ApiVuelos.Services;
using ApiVuelos.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVuelos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AerolineaController : ControllerBase
    {
        private readonly IValidator<AerolineaDto> _validator;
        private readonly ICommonService<AerolineaDto, AerolineaDto, AerolineaDto> _Service;
        public AerolineaController(IValidator<AerolineaDto> validator,
            [FromKeyedServices("AerolineService")] ICommonService<AerolineaDto, AerolineaDto, AerolineaDto> service)
        {
            _validator = validator;
            _Service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<AerolineaDto>> Get() =>
            await _Service.Get();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AerolineaDto>> GetById(int id)
        {
            var aerolineaDto = await _Service.GetById(id);
            return aerolineaDto == null ? NotFound() : Ok(aerolineaDto);
        }

        [HttpPost]
        public async Task<ActionResult<AerolineaDto>> Add([FromBody] AerolineaDto addAerolineaDto)
        {
            var validationResult = await _validator.ValidateAsync(addAerolineaDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var aerolineaDto = await _Service.Add(addAerolineaDto);
            return CreatedAtAction(nameof(GetById), new { id = aerolineaDto.IdAerolinea }, aerolineaDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AerolineaDto>> Update([FromBody] AerolineaDto modAerolineaDto, int id)
        {
            var validationResult = await _validator.ValidateAsync(modAerolineaDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var aerolineaDto = await _Service.Update(id, modAerolineaDto);
            return aerolineaDto == null ? NotFound() : Ok(aerolineaDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AerolineaDto>> Delete(int id)
        {
            var aerolineaDto = await _Service.Delete(id);
            return aerolineaDto == null ? NotFound() : Ok(aerolineaDto);
        }

    }
}
