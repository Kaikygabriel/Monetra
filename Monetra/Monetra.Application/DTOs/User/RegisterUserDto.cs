using System.ComponentModel.DataAnnotations;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.DTOs.User;

public class RegisterUserDto
{
    [StringLength(70,MinimumLength = 3)]
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public static implicit operator Domain.BackOffice.Entities.User(RegisterUserDto model)
        => new (model.Password, new Email(model.Email));
}