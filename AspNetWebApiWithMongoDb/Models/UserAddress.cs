using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace AspNetWebApiWithMongoDb.Models;

public class UserAddress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("country_name")]
    public string Country { get; set; }
    
    [BsonElement("city_name")]
    public string City { get; set; }
    
    [BsonElement("address_line_1")]
    public string AddressLine1 { get; set; }
    
    [BsonElement("address_line_2")]
    public string AddressLine2 { get; set; }

    public UserAddress()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}