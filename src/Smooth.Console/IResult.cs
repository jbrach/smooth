using System;
using System.Collections.Generic;

namespace Smooth
{
    public interface IResult
    {
        List<string> Errors {get;}
        bool HasErrors { get;  }
    }

    public class SmoothResult : IResult
    {
        public bool HasErrors {get; private set;}

        public List<string> Errors {get; private set;}

        public SmoothResult(List<string> errors)
        {
            HasErrors = (errors.Count>0);
            Errors = errors;
        }
    }
}