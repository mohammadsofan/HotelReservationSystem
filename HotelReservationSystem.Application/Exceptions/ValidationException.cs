using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Exceptions
{
    public class ValidationException:Exception
    {
        public ICollection<ValidationError> Errors { get; set; }

        public ValidationException(ICollection<ValidationError> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
    public class ValidationError
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }

        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}
