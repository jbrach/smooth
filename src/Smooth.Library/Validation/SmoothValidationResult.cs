using System.Collections.Generic;

namespace Smooth.Library
{
    public class SmoothValidationResult : IValidationResult
    {
        public bool HasErrors {get; private set;}

        public List<string> Errors {get; private set;}

        public SmoothValidationResult(List<string> errors)
        {
            HasErrors = (errors.Count>0);
            Errors = errors;
        }
    }
}