namespace AspNetWebApiWithMongoDb.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string[] PhoneNumbers { get; set; }
    public IList<UserAddressDto> Addresses { get; set; }
}