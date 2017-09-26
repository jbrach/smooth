using Xunit;
using Smooth.Library;
using Smooth.Library.Extensions;
using Smooth.Library.FileNaming;
using Tests.TestingHelpers;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Mongo2Go;


using System.Threading;
using Mongo2Go.Helper;
using MongoDB.Driver.Linq;

namespace Test.Database
{
    public class StartingMongoTests 
    {
        [Fact]
        public void ConnectLocalMongo()
        {
           var context =   MongoContext.MongoContextFactory();
           context.AddData("Joe");
           context.AddData("Jim");
           var results = context.GetData();

            Assert.NotEmpty(results.Where(x=>x.ImageName=="Joe"));
            Assert.NotEmpty(results.Where(x=>x.ImageName=="Jim"));

           foreach(var image in results.Where(x=>x.ImageName=="Joe"))
           {
               context.Delete(image.Id);
           }
           foreach(var image in results.Where(x=>x.ImageName=="Jim"))
           {
               context.Delete(image.Id);
           }
        }
    }

   /* [Subject("Runner Integration Test")]
    public class when_using_the_inbuild_serialization : MongoIntegrationTest
    {
        static SmoothData findResult;

        Establish context = () =>
            {
                CreateConnection();
                _collection.Insert(TestDocument.DummyData1());
            };

        Because of = () => findResult = _collection.FindOneAs<TestDocument>();

        It should_return_a_result = () => findResult.ShouldNotBeNull();
        It should_hava_expected_data = () => findResult.ShouldHave().AllPropertiesBut(d => d.Id).EqualTo(TestDocument.DummyData1());

        Cleanup stuff = () => _runner.Dispose();
    }*/

    public class MongoIntegrationTest
    {
        internal static MongoDbRunner _runner;
        internal static IMongoCollection<SmoothData> _collection;

        internal static void CreateConnection()
        {
            _runner = MongoDbRunner.Start();

            var server = new MongoClient(_runner.ConnectionString);
            IMongoDatabase database = server.GetDatabase("IntegrationTest");
            _collection = database.GetCollection<SmoothData>("TestCollection");
        }
    }
    public class MongoContext 
    {
        private IMongoDatabase Database;
        
        private MongoContext()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            Database = client.GetDatabase("smooth");
        }

        public static MongoContext MongoContextFactory()
        {
            var context = new Lazy<MongoContext>(() => new MongoContext());
            return context.Value;
        }
        public IList<SmoothData> GetData()
        {
            var collection = Database.GetCollection<SmoothData>("SmoothData");
            return collection.Find(new BsonDocument()).ToList();

        }

        public  void AddData(string name)
        {
            var data = new SmoothData() { Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString() ,ImageName = name };
            Database.GetCollection<SmoothData>("SmoothData").InsertOne(data);
        }

          public  void Delete(string id)
        {
            Database.GetCollection<SmoothData>("SmoothData").DeleteOne(x=>x.Id==id);
        }
        
        
    }

    
    public class SmoothData
    {
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }
  
        [BsonElement]
        public string ImageName { get; set; }
      
    }
}