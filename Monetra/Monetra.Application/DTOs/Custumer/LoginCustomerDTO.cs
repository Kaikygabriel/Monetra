using System.ComponentModel.DataAnnotations;

namespace Monetra.Application.DTOs.Custumer;

public class LoginCustomerDTO
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(100,MinimumLength = 3)]
    public string Password { get; set; }
}