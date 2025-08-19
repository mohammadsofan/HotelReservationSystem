using FluentValidation;
using HotelReservationSystem.Application.Exceptions;
using MediatR;
namespace HotelReservationSystem.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
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
                throw new Exceptions.ValidationException(errors);
            }

            var response = await next(cancellationToken);

            return response;
        }
    }
}
