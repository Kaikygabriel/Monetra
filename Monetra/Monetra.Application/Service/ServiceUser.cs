using Monetra.Application.DTOs.User;
using Monetra.Application.Service.Abstraction;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Org.BouncyCastle.Crypto.Generators;

namespace Monetra.Application.Service;

public class ServiceUser : IServiceUser
{
    private IUnitOfWork _unitOfWork;

    public ServiceUser(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public bool CheckPassword(User user, string password)
        => BCrypt.Net.BCrypt.Verify(password, user.Password);

    public async Task<bool> UserExisting(RegisterUserDto model)
    {
        if (await _unitOfWork.UserRepository.GetByPredicate(x => x.Email.Address == model.Email) is not null)
            return true;
        return false;
    }

    public void AddHashPassword(User user)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.UpdatePassword(passwordHash);
    }
}