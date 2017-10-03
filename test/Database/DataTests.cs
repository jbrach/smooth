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
using Smooth.Library.Identification;

namespace Test.Database
{
    public class StartingMongoTests
    {


        //[Fact]
        public void ConnectLocalMongo()
        {
            //TODO Inject MongoContextFactory.   This layer should not know about the database or should it?  
            // ContextFactory.BuildContext
            using (var context = new MongoTestContext())
            {
                IImageRepository repository = new MongoImageRepository(context);

                repository.AddData("Joe");
                repository.AddData("Jim");
                var results = repository.GetData();

                Assert.NotEmpty(results.Where(x => x.ImageName == "Joe"));
                Assert.NotEmpty(results.Where(x => x.ImageName == "Jim"));

                foreach (var image in results.Where(x => x.ImageName == "Joe"))
                {
                    repository.Delete(image.Id);
                }
                foreach (var image in results.Where(x => x.ImageName == "Jim"))
                {
                    repository.Delete(image.Id);
                }
            }
        }
    }


    public class MongoTestContext : IDisposable, IMongoDatabaseContext
    {

        public IMongoDatabase Database {get; private set;}
        private MongoDbRunner _runner;

        public MongoTestContext()
        {
            _runner = MongoDbRunner.Start();
            //TODO how to inject the connection string 
            var client = new MongoClient(_runner.ConnectionString);
            // var client = new MongoClient("mongodb://127.0.0.1:27017");
            Database = client.GetDatabase("smooth");
        }


        public void Dispose()
        {
            _runner.Dispose();
        }
    }

    
}