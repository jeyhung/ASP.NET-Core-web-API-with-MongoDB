using AspNetWebApiWithMongoDb.Models;

namespace AspNetWebApiWithMongoDb.Dtos;

public class UserSearchDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? StartDateOfBirth { get; set; }
    public DateTime? EndDateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}