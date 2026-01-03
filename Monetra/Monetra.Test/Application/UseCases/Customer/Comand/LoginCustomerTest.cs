using Monetra.Application.DTOs.Custumer;
using Monetra.Application.Service;
using Monetra.Application.UseCases.Customer.Command.Handlers;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Customer.Comand;

public class LoginCustomerTest
{
    private readonly LoginCustomerHandler _handler = new (new FakeUnitOfWork(),new FakeTokenService(),new FakeMediator(),new ServiceUser(new FakeUnitOfWork()));
    [Fact]
    public async Task Should_Return_False_If_Customer_Does_Not_Exist()
    {
        var request = new LoginCustomerRequest(new LoginCustomerDTO()
        {
            Email = "teste",
            Password = "teste2"
        });
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
        
    }
    
    [Fact]
    public async Task Should_Return_False_If_PasswordIs_NotEquals_CustomerPassword()
    {
        var request = new LoginCustomerRequest(new LoginCustomerDTO()
        {
            Email = "test@monetra.com",
            Password = "teste2"
        });
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
    [Fact]
    public async Task Should_Return_True_PasswordIsValid_AndCustomerExisting()
    {
        var request = new LoginCustomerRequest(new LoginCustomerDTO()
        {
            Email = "maria@email.com",
            Password = "12345"
        });
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
}