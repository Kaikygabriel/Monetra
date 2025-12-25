using Monetra.Application.DTOs.User;

namespace Monetra.Application.Service.Abstraction;

public interface IServiceUser
{
    Task<bool> TryCreateAsync(RegisterUserDto model);
}