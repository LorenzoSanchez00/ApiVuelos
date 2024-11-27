using ApiVuelos.DTOs;
using ApiVuelos.Models;
using FluentValidation;

namespace ApiVuelos.Validators
{
    public class AerolineaValidator : AbstractValidator<AerolineaDto>
    {
        public AerolineaValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe ingresar el nombre de la Aerolinea!");
        }
    }
}
