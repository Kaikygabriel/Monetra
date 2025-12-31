using MediatR;
using Monetra.Application.DTOs.Custumer;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Application.UseCases.Customer.Command.Response;
using Monetra.Application.UseCases.Customer.Notification.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Customer.Command.Handlers;

public class LoginCustomerHandler : HandlerCustomerBase,IRequestHandler<LoginCustomerRequest,Result<CustomerAuthResponse>>
{
    private IMediator _mediator;
    public LoginCustomerHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IMediator mediator,IServiceUser  serviceUser)
        : base(unitOfWork, tokenService, serviceUser)
    {
        _mediator = mediator;
    }

    public async Task<Result<CustomerAuthResponse>> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
    {
        return  await LoginCustomer(request.Model);
    }
    private async Task<Result<CustomerAuthResponse>> LoginCustomer(LoginCustomerDTO request)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByEmail(request.Email);
        if (customer is null )
            return Result<CustomerAuthResponse>.Failure(Errors.CustumerNoExisting); 
        if(PasswordIsValid(customer.User,request.Password))    
            return Result<CustomerAuthResponse>.Failure(Errors.PasswordInvalid);
        
        // await _mediator.Publish(new CustumerAuthNotification
        //     (customer,"Bem Vindo!",MenssagesEmail.LoginCustomerMenssage(customer.User.Email.Address)));
        var response = new CustomerAuthResponse(GenerateJwtTokenOfCustomer(customer), customer.Id);
        return Result<CustomerAuthResponse>.Success(response);
    }

    private bool PasswordIsValid(User user, string password)
        => !_serviceUser.CheckPassword(user,password);
}