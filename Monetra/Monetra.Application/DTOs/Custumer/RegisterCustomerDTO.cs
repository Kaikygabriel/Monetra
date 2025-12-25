using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Application.DTOs.Custumer;

public class RegisterCustomerDTO
{
    [Required]
    [StringLength(100,MinimumLength = 2,ErrorMessage = "Name is small")]
    public string Name { get; set; }
    [Required]
    public User.RegisterUserDto UserDto { get; set; }

    public static implicit operator Customer(RegisterCustomerDTO model)
        => new(model.UserDto,model.Name);
}