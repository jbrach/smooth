using Xunit;
using Smooth.Library;
using Smooth.Library.Extensions;
using Smooth.Library.FileNaming;
using Tests.TestingHelpers;
using MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Test.Database
{
    public class StartingMongoTests
    {
        [Fact]
        public void ConnectLocalMongo()
        {
           Assert.True(true);
            
        }
    }

    public class MongoContext
    {
        public IMongoDatabase Database;
        
        public MongoContext()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            Database = client.GetDatabase("smooth");
        }

        public IMongoCollection<SmoothData> GetData()
        {
            return Database.GetCollection<SmoothData>("");
        }

        public  void AddData(string name)
        {
            var data = new SmoothData() { ImageName = name};
            
        }
    }

    public class SmoothData
    {
        public string ImageName { get; set; }
      
    }
}