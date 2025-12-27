using MediatR;
using Microsoft.AspNetCore.Http;
using Monetra.Application.Commum;
using Monetra.Application.DTOs.Custumer;
using Monetra.Application.Service;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases.Custumer.Command.Request;
using Monetra.Application.UseCases.Custumer.Notification;
using Monetra.Application.UseCases.Custumer.Notification.Request;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Custumer.Command.Handlers;

public class LoginCustomerHandler : HandlerCustomerBase,IRequestHandler<LoginCustomerRequest,Result<string>>
{
    private IMediator _mediator;
    public LoginCustomerHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IMediator mediator,IServiceUser  serviceUser)
        : base(unitOfWork, tokenService, serviceUser)
    {
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
    {
        return  await LoginCustomer(request.Model);
    }
    private async Task<Result<string>> LoginCustomer(LoginCustomerDTO request)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByEmail(request.Email);
        if (customer is null || PasswordIsValid(customer.User,request.Password))
            return Result<string>.Failure(Errors.PasswordInvalid);
        
        await _mediator.Publish(new CustumerAuthNotification(customer,"Bem Vindo!"));

        return Result<string>.Sucess(GenerateJwtTokenOfCustomer(customer));
    }

    private bool PasswordIsValid(User user, string password)
        => !_serviceUser.CheckPassword(user,password);
}