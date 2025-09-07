using FluentValidation;
using HotelReservationSystem.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting validation for {RequestType}", typeof(TRequest).Name);
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context,cancellationToken)));
            var errors = validationFailures
                  .Where(validationResult => !validationResult.IsValid)
                  .SelectMany(validationResult => validationResult.Errors)
                  .Select(validationFailure => new ValidationError(
                      validationFailure.PropertyName,
                      validationFailure.ErrorMessage))
                  .ToList();

            if (errors.Any())
            {
                _logger.LogWarning("Validation failed for {RequestType} with {ErrorCount} errors.", typeof(TRequest).Name, errors.Count);
                throw new Exceptions.ValidationException(errors);
            }

            _logger.LogInformation("Validation passed for {RequestType}", typeof(TRequest).Name);
            var response = await next(cancellationToken);

            return response;
        }
    }
}
