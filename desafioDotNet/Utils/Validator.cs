using desafioDotNet.Models;
using FluentValidation;

namespace desafioDotNet.Utils {
    public class Validator : AbstractValidator<RegisterModel> {
        public Validator() {
            RuleFor(p => p.Cpf)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .MaximumLength(11)
                .MinimumLength(11)
                .WithMessage("O numero que caracteres no CPF deve ser igual a 11")
                .Must(cpf => cpf.Length > 0 && !cpf[0].Equals("0"))
                .WithMessage("O CPF nao pode começar com 0.");
        }
    }
}
