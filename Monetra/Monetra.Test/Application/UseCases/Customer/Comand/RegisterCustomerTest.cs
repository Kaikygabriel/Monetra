using Monetra.Application.DTOs.Custumer;
using Monetra.Application.DTOs.User;
using Monetra.Application.Service;
using Monetra.Application.UseCases.Customer.Command.Handlers;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Customer.Comand;

public class RegisterCustomerTest
{
    private readonly RegisterCustomerHandler _registerCustomerHandler;

    public RegisterCustomerTest()
    {
        _registerCustomerHandler = new(
            new FakeUnitOfWork(),
            new FakeMediator(),
            new FakeTokenService(),
            new ServiceUser(new FakeUnitOfWork()));
    }

    [Fact]
    public async Task Should_Return_False_If_User_Exists()
    {
        var request = new RegisterCostumerRequest(new RegisterCustomerDTO()
        {
            Name = "teste",
            UserDto = new RegisterUserDto()
            {
                Email = "admin@monetra.com",
                Password = "123456"
            }
        });
        var response = await _registerCustomerHandler.Handle(request, CancellationToken.None);
        Assert.False(response.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_False_If_Customer_Exists()
    {
        var request = new RegisterCostumerRequest(new RegisterCustomerDTO()
        {
            Name = "João Silva",
            UserDto = new RegisterUserDto()
            {
                Email = "teste@email.com",
                Password = "teste123"
            }
        });
        var response = await _registerCustomerHandler.Handle(request, CancellationToken.None);
        Assert.False(response.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_True_if_UserANdCustomerIsValid()
    {
        var request = new RegisterCostumerRequest(new RegisterCustomerDTO()
        {
            Name = "teste2",
            UserDto = new RegisterUserDto()
            {
                Email = "teste2@email.com",
                Password = "teste123"
            },
            DescriptionExpense = "teste",
            Salary = 10
        });
        var response = await _registerCustomerHandler.Handle(request, CancellationToken.None);
        Assert.True(response.IsSuccess);
    }
}