using AspNetWebApiWithMongoDb.Models;

namespace AspNetWebApiWithMongoDb.Dtos;

public class UserCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string[] PhoneNumbers { get; set; }
    public IList<UserAddressCreateDto> Addresses { get; set; }
}