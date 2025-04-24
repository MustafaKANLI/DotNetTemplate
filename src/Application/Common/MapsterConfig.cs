using DotNetTemplate.Application.DTOs;
using DotNetTemplate.Domain.Entities;
using Mapster;

namespace DotNetTemplate.Application.Common;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<User, UserDto>.NewConfig();
        TypeAdapterConfig<CreateUserDto, User>.NewConfig();
        TypeAdapterConfig<UserContact, UserContactDto>.NewConfig();
        TypeAdapterConfig<CreateUserContactDto, UserContact>.NewConfig();
    }
}
