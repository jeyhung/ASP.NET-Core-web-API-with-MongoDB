using System.Text.Json.Serialization;
using AspNetWebApiWithMongoDb.Models;

namespace AspNetWebApiWithMongoDb.Dtos;

public class UserUpdateDto 
{
    [JsonIgnore]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string[] PhoneNumbers { get; set; }
    public IList<UserAddressDto> Addresses { get; set; }
}