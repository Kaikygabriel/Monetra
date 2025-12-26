using Monetra.Application.DTOs.User;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Application.Service.Abstraction;

public interface IServiceUser
{
    bool CheckPassword(User user, string password);
    Task<bool> TryCreateAsync(RegisterUserDto model);
}