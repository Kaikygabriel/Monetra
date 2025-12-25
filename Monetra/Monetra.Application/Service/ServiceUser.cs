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

    public async Task<bool> TryCreateAsync(RegisterUserDto model)
    {
        try
        {
            return await CreateUser(model);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private async Task<bool> CreateUser(RegisterUserDto model)
    {
        if (await UserExisting(model.Email))
            return false;
        User user = model;
        CreateHashPassword(user);
        _unitOfWork.UserRepository.Create(user);
        return true;
    }

    private async Task<bool> UserExisting(string email)
    {
        if (await _unitOfWork.UserRepository.GetByPredicate(x => x.Email.Address == email) is not null)
            return true;
        return false;
    }

    private void CreateHashPassword(User user)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.UpdatePassword(passwordHash);
    }
}