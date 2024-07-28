using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            SetUpValidator();
        }

        private void SetUpValidator()
        {
            //Não é uma boa prática chumbar as mensagens de retorno diretamente aqui, pois se a mensagem no futuro for alterada, precisaremos catar ela em cada parte do código o que iria complicar a manutenção desse código.
            //A ideia é colocar essas mensagens no projeto de Exception num arquivo .resw
            RuleFor(request => request.Name).NotEmpty().WithMessage($"{ResourceErrorMessages.NAME_EMPTY} empty.");
            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage($"{ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY} later than today ({DateTime.UtcNow}).");
            RuleFor(request => request)
                .Must(request => request.EndDate.Date >= request.StartDate.Date)
                .WithMessage($"{ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE} later start date.");
        }
    }
}
