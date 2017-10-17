using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Smooth.Library.Identification
{
    public class MongoImageRepository : IImageRepository
    {
     
        private IMongoDatabaseContext Context { get; }
        public MongoImageRepository(IMongoDatabaseContext context)
        {
            this.Context = context;
        }


        public IList<SmoothData> GetData()
        {
            var collection = Context.Database.GetCollection<SmoothData>("SmoothData");
            return collection.Find(new BsonDocument()).ToList();

        }

        public void AddData(string name)
        {
            var data = new SmoothData { Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(), ImageName = name };
            Context.Database.GetCollection<SmoothData>("SmoothData").InsertOne(data);
        }

        public void Delete(string id)
        {
            Context.Database.GetCollection<SmoothData>("SmoothData").DeleteOne(x => x.Id == id);
        }

    }

}