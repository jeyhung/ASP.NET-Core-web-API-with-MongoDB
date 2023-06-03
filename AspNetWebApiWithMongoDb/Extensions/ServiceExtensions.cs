using AspNetWebApiWithMongoDb.Dtos;
using AspNetWebApiWithMongoDb.Mappers;
using AspNetWebApiWithMongoDb.Services;
using AspNetWebApiWithMongoDb.Validators;
using AutoMapper;
using FluentValidation;

namespace AspNetWebApiWithMongoDb.Extensions;

public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            RegisterMappers(services);
            RegisterServices(services);
            RegisterValidators(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        private static void RegisterMappers(IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<UserCreateDto>, UserCreateValidator>();
            services.AddTransient<IValidator<UserUpdateDto>, UserUpdateValidator>();
            
            services.AddTransient<IValidator<UserAddressCreateDto>, UserAddressCreateValidator>();
            services.AddTransient<IValidator<UserAddressDto>, UserAddressUpdateValidator>();
        }
    }