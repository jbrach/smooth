using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Smooth.Library.Identification
{
    public class SmoothData
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string ImageName { get; set; }

    }

}