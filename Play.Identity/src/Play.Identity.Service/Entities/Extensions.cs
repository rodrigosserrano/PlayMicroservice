using Play.Common.Auth.DTOs;
using Play.Identity.Service.DTOs.Role;
using Play.Identity.Service.DTOs.User;

namespace Play.Identity.Service.Entities;

public static class Extensions
{
    public static UserDTO AsDTO(this User user)
    {
        return new UserDTO(
            Id: user.Id,
            Name: user.Name,
            Nickname: user.Nickname,
            Email: user.Email,
            Password: user.Password,
            Balance: user.Balance,
            Role: user.Role
        );
    }

    public static IdentityDTO AsIdentity(this User user)
    {
        return new IdentityDTO(
            Id: user.Id,
            Name: user.Name,
            Email: user.Email,
            Role: user.Role
        );
    }

    public static RoleDTO AsDTO(this Role role)
    {
        return new RoleDTO(
            Id: role.Id,
            Name: role.Name
        );
    }
}