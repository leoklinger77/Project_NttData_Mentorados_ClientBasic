using FluentValidation;

namespace Valhalla.Dominio.Models.Validation
{
    public class PhoneValidation : AbstractValidator<Phone>
    {
        public PhoneValidation()
        {
            RuleFor(x => x.Ddd)
                   .NotEmpty()
                   .WithMessage("O ddd é obrigatorio")
                   .Length(2, 2)
                   .WithMessage("O ddd deve conter 2 caracteres");

            When(x=>x.PhoneType == Enum.PhoneType.Smarthphone, () => 
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O celular é obrigatorio")
                   .Length(9, 9)
                   .WithMessage("O celular deve conter 9 caracteres");
            });

            When(x => x.PhoneType == Enum.PhoneType.Home, () =>
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O telefone fixo é obrigatorio")
                   .Length(8, 8)
                   .WithMessage("O telefone fixo deve conter 8 caracteres");
            });

            When(x => x.PhoneType == Enum.PhoneType.Commercial, () =>
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O telefone comercial é obrigatorio")
                   .Length(8, 9)
                   .WithMessage("O telefone comercial deve conter entre 8 e 9 caracteres");
            });


        }
    }
}
