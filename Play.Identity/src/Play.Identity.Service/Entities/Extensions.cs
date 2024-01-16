using Play.Identity.Service.DTOs;

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
            Balance: user.Balance
        );
    }
}