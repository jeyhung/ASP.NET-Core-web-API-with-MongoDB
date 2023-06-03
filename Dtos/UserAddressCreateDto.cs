namespace AspNetWebApiWithMongoDb.Dtos;

public class UserAddressCreateDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
}