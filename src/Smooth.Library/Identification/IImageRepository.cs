using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;

namespace Smooth.Library.Identification
{

    public interface IImageRepository
    {
         void Delete(string id);
         void AddData(string name);
         IList<SmoothData> GetData();
    }

}