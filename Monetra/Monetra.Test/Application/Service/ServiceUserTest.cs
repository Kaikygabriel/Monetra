using Monetra.Application.DTOs.User;
using Monetra.Application.Service;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.ObjectValues;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.Service;

public class ServiceUserTest 
{
    private readonly ServiceUser _serviceUser = new(new FakeUnitOfWork());
    private readonly FakeUserRepository _repositoryUser = new();
    [Fact]
    public async Task Should_Return_true_if_user_is_Existing()
    {
        var userExisting = new RegisterUserDto{Email = "admin@monetra.com",Password = "teste.com"};
        var result = await _serviceUser.UserExisting(userExisting);
        Assert.True(result); 
    }
    [Fact]
    public async Task Should_Return_false_if_user_is_NoExisting()
    {
        var userExisting = new RegisterUserDto{Email = "John@gmail.com",Password = "teste@gmail.com"};
        var result = await _serviceUser.UserExisting(userExisting);
        Assert.False(result); 
    }
    [Fact]
    public async Task Should_Return_True_if_PasswordIsEquals()
    {
        var password = "12345dadad@a";
        var user = new User(password, new Email("admin@monetra.com"));
        _serviceUser.AddHashPassword(user);
        var result =  _serviceUser.CheckPassword(user,password);
        Assert.True(result); 
    }
    [Fact]
    public async Task Should_Return_False_if_PasswordIsNoEquals()
    {
        var password2 = "fdjsadçlfja";
        var password = "12345671dafd";
        var user = new User(password, new Email("admin@monetra.com"));
        _serviceUser.AddHashPassword(user);
        var result =  _serviceUser.CheckPassword(user,password2);
        Assert.False(result); 
    }
}