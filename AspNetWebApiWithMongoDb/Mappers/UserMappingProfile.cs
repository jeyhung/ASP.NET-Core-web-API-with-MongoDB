using AspNetWebApiWithMongoDb.Dtos;
using AspNetWebApiWithMongoDb.Models;
using AutoMapper;

namespace AspNetWebApiWithMongoDb.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserListItemDto>();
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();

        CreateMap<UserAddressCreateDto, UserAddress>();
        CreateMap<UserAddressDto, UserAddress>()
            .ReverseMap();
    }
}