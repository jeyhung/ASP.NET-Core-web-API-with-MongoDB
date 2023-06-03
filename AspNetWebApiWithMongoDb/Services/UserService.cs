using System.Linq.Expressions;
using AspNetWebApiWithMongoDb.Common;
using AspNetWebApiWithMongoDb.Dtos;
using AspNetWebApiWithMongoDb.Models;
using AutoMapper;
using FluentValidation;
using MongoDB.Driver;

namespace AspNetWebApiWithMongoDb.Services;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IValidator<UserCreateDto> _userCreateValidator;
    private readonly IValidator<UserUpdateDto> _userUpdateValidator;
    private readonly IMapper _mapper;

    public UserService(IMongoClient mongoClient,
        MongoDbSettings settings,
        IValidator<UserCreateDto> userCreateValidator,
        IValidator<UserUpdateDto> userUpdateValidator,
        IMapper mapper)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _usersCollection = database.GetCollection<User>(settings.CollectionName);
        _userCreateValidator = userCreateValidator;
        _userUpdateValidator = userUpdateValidator;
        _mapper = mapper;
    }

    public async Task<PagedResultRequestDto<UserListItemDto>> GetUsersAsync(UserSearchDto userSearchDto)
    {
        FilterDefinition<User> filters = Builders<User>.Filter.Empty;

        if (userSearchDto.FirstName != null)
        {
            FilterDefinition<User> firstNameFilter =
                Builders<User>.Filter.Eq(u => u.FirstName, userSearchDto.FirstName);
            filters = Builders<User>.Filter.And(filters, firstNameFilter);
        }

        if (userSearchDto.LastName != null)
        {
            FilterDefinition<User> lastNameFilter = Builders<User>.Filter.Eq(u => u.LastName, userSearchDto.LastName);
            filters = Builders<User>.Filter.And(filters, lastNameFilter);
        }

        if (userSearchDto.Gender.HasValue)
        {
            FilterDefinition<User> genderFilter = Builders<User>.Filter.Eq(u => u.Gender, userSearchDto.Gender.Value);
            filters = Builders<User>.Filter.And(filters, genderFilter);
        }

        if (userSearchDto.StartDateOfBirth.HasValue)
        {
            FilterDefinition<User> startDateOfBirthFilter =
                Builders<User>.Filter.Gt(u => u.DateOfBirth.Date, userSearchDto.StartDateOfBirth.Value.Date);
            filters = Builders<User>.Filter.And(filters, startDateOfBirthFilter);
        }

        if (userSearchDto.EndDateOfBirth.HasValue)
        {
            FilterDefinition<User> endDateOfBirthFilter =
                Builders<User>.Filter.Gt(u => u.DateOfBirth.Date, userSearchDto.EndDateOfBirth.Value.Date);
            filters = Builders<User>.Filter.And(filters, endDateOfBirthFilter);
        }

        IList<User> users = await _usersCollection.Find<User>(filters)
            .Skip(userSearchDto.Skip)
            .Limit(userSearchDto.Limit)
            .ToListAsync();

        return new PagedResultRequestDto<UserListItemDto>()
        {
            Items = _mapper.Map<IReadOnlyList<UserListItemDto>>(users),
            TotalCount = await _usersCollection.CountDocumentsAsync(filters)
        };
    }

    public async Task<IList<UserAddressDto>> GetUserAddressesAsync(string userId)
    {
        User user = await GetUserByIdInternalAsync(userId);
        return _mapper.Map<IList<UserAddressDto>>(user.Addresses);
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        User user = await GetUserByIdInternalAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<string> CreateUserAsync(UserCreateDto userCreateDto)
    {
        await _userCreateValidator.ValidateAndThrowAsync(userCreateDto);
        User user = _mapper.Map<User>(userCreateDto);
        user.CreatedDate = DateTime.Now;
        await _usersCollection.InsertOneAsync(user);
        return user.Id;
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        await _userUpdateValidator.ValidateAndThrowAsync(userUpdateDto);
        User user = await GetUserByIdInternalAsync(userUpdateDto.Id);
        user.Addresses = new List<UserAddress>();
        _mapper.Map(userUpdateDto, user);
        user.UpdatedDate = DateTime.Now;
        await _usersCollection.ReplaceOneAsync(e => e.Id == userUpdateDto.Id, user);
    }

    public async Task DeleteAsync(string id)
    {
        User user = await GetUserByIdInternalAsync(id);
        await _usersCollection.DeleteOneAsync(e => e.Id == id);
    }

    private async Task<User> GetUserByIdInternalAsync(string id)
    {
        User user = await _usersCollection.FindSync(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new DataNotFoundException(string.Format($"User with Id {id} not found!"));
        }

        return user;
    }
}