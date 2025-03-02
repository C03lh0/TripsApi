﻿using FluentValidation.Results;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JorneyDbContext();

            var trip = dbContext
                .Trips
                .FirstOrDefault(x => x.Id == tripId);

            Validate(trip, request);

            var entity = new Activity
            {
                Name = request.Name,
                Date = request.Date,
                TripId = tripId,
            };

            dbContext.Activities.Add(entity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
                Status = (ActivityStatus)entity.Status,
            };
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if (trip is null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            var validator = new RegisterActivityValidator();

            var result = validator.Validate(request);

            if (!(request.Date >= trip.StartDate && request.Date <= trip.EndDate))
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
