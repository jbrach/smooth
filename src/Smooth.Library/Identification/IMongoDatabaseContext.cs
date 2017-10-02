using MongoDB.Driver;

namespace Smooth.Library.Identification
{
    public interface IMongoDatabaseContext
    {
          IMongoDatabase Database{ get;}

    }

}