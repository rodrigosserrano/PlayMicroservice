using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Identity.Service.DTOs;

public record UpdateUserDTO(
    string Name,
    string Nickname,
    [EmailAddress] string Email,
    [PasswordPropertyText, MinLength(8)] string OldPassword,
    [PasswordPropertyText, MinLength(8)] string NewPassword,
    double Balance
);