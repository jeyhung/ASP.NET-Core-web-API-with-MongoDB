using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspNetWebApiWithMongoDb.Models;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("first_name")]
    public string FirstName { get; set; }
    
    [BsonElement("last_name")]
    public string LastName { get; set; }

    [BsonElement("gender")]
    public Gender Gender { get; set; }

    [BsonElement("date_of_birth")]
    public DateTime DateOfBirth { get; set; }

    [BsonElement("phone_numbers")]
    public string[] PhoneNumbers { get; set; }
    
    [BsonElement("user_addresses")]
    public IList<UserAddress> Addresses { get; set; }

    [BsonElement("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [BsonElement("last_updated_date")]
    public DateTime UpdatedDate { get; set; }
}