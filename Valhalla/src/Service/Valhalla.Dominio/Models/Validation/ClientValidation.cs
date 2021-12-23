using FluentValidation;
using Valhalla.Dominio.Tools;

namespace Valhalla.Dominio.Models.Validation
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("O nome completo é obrigatorio")
                .Length(5, 100)
                .WithMessage("o campo nome deve conter entre 5 e 100 caracteres");

            When(x => x.PersonType == Enum.PersonType.Juridical, () =>
            {
                RuleFor(x => x.Document.IsCnpj())
                    .Equal(true)
                    .WithMessage("Cnpj é invalido");
            });

            When(x => x.PersonType == Enum.PersonType.Physical, () =>
            {
                RuleFor(x => x.Document.IsCpf())
                    .Equal(true)
                    .WithMessage("Cpf é invalido");
            });

        }
    }
}
