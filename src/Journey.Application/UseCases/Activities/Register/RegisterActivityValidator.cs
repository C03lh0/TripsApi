using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Register
{
    internal class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        // O validator só é responsável por validar os dados da requisiçã
        //E porque não validamos a data aqui tbm? Pq sua validação depende de outra propriedade, e não podemos misturar respon-
        //sabilidades.
        public RegisterActivityValidator()
        {
            SetUpValidator();
        }

        private void SetUpValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage($"{ResourceErrorMessages.NAME_EMPTY} empty.");
        }
    }
}
