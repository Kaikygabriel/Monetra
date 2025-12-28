using Monetra.Application.DTOs.User;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Application.Service.Abstraction;

public interface IServiceUser
{
    bool CheckPassword(User user, string password);
    Task<bool> UserExisting(RegisterUserDto model);
    void AddHashPassword(User user);
}