using System.Collections.Generic;

namespace Smooth
{
    public interface ICommand 
    {
        List<string> Validate();
        bool Run();

    }
}