using FluentValidation;
using ProyectoJWT.Dtos;

namespace ProyectoJWT.Validator
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario).NotNull().WithMessage("El usuario no puede ser nulo");
            RuleFor(usuario => usuario.id).GreaterThan(0).WithMessage("El id no puede ser menor que 0");
            RuleFor(usuario => usuario.name).Length(0, 15).WithMessage("Nombre incorrecto");
            RuleFor(usuario => usuario.pass).MinimumLength(8).WithMessage("La contraseña es muy corta");
            RuleFor(usuario => usuario.pass).Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .Matches(@"[\!\?\*\.]+").WithMessage("La contraseña es muy debil");
        }
    }
}
