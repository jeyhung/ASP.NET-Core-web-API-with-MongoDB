using AspNetWebApiWithMongoDb.Common;
using AspNetWebApiWithMongoDb.Dtos;

namespace AspNetWebApiWithMongoDb.Services;

public interface IUserService
{
    Task<PagedResultRequestDto<UserListItemDto>> GetUsersAsync(UserSearchDto userSearchDto);
    Task<IList<UserAddressDto>> GetUserAddressesAsync(string userId);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<string> CreateUserAsync(UserCreateDto userCreateDto);
    Task UpdateUserAsync(UserUpdateDto userUpdateDto);
    Task DeleteAsync(string id);
}