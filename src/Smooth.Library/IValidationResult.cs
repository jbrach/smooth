using System.Collections.Generic;

namespace Smooth.Library
{
    public interface IValidationResult
    {
        List<string> Errors {get;}
        bool HasErrors { get;  }
    }
}