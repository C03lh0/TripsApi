using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request) 
        {
            ValidateRequestRegisterTrip(request);

            var dbContext = new JorneyDbContext();

            var trip = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(trip);
            //Persite os dados no banco.
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
            };
        }

        private void ValidateRequestRegisterTrip(RequestRegisterTripJson request) 
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                //Não é uma boa prática chumbar as mensagens de retorno diretamente aqui, pois se a mensagem no futuro for alterada, precisaremos catar ela em cada parte do código o que iria complicar a manutenção desse código.
                //A ideia é colocar essas mensagens no projeto de Exception num arquivo .resw
                throw new ErrorOnValidationException($"{nameof(request.Name)}: {ResourceErrorMessages.NAME_EMPTY} empty.");

            if (request.StartDate.Date < DateTime.UtcNow.Date)
                throw new ErrorOnValidationException($"{ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY} later than today ({DateTime.UtcNow}).");

            if (request.EndDate.Date < request.StartDate.Date)
                throw new ErrorOnValidationException($"{ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE} later start date ({request.StartDate}).");

        }   
    }
}
