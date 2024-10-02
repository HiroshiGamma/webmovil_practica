using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.Dtos;
using api01.source.models;

namespace api01.source.Mappers
{
    public static class UserMapper
    {
        
        public static UserDto toUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                RolId = userModel.RolId,
                Rol = userModel.Rol
            };
        }

        public static User toUserFromCreatoDto(this CreateUserRequestDto createUserRequestDto)
        {
            return new User
            {
                Name = createUserRequestDto.Name,
                Rut = createUserRequestDto.Rut,
                Email = createUserRequestDto.Email,
                RolId = createUserRequestDto.RolId
            };
        }
        
    }
}