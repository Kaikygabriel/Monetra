using MediatR;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Application.UseCases.Customer.Notification.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Customer.Command.Handlers;

public class RegisterCustomerHandler:HandlerCustomerBase, IRequestHandler<RegisterCostumerRequest,Result<string>>
{
    private readonly IMediator _mediator;
    public RegisterCustomerHandler(IUnitOfWork unitOfWork, IMediator mediator,ITokenService tokenService,IServiceUser  serviceUser) 
        : base(unitOfWork,tokenService, serviceUser)
    {
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(RegisterCostumerRequest request, CancellationToken cancellationToken)
    {
        return await CreateCustomer(request);
    }

    private async Task<Result<string>> CreateCustomer(RegisterCostumerRequest request)
    {
        if (await CustomerExisting(request.Model.Name))
            return Result<string>.Failure(Errors.UserCreatedFalid);
        
        if (await _serviceUser.UserExisting(request.Model.UserDto))
            return Result<string>.Failure(Errors.InvalidEmail);
        
        Domain.BackOffice.Entities.Customer customer = request.Model;
        _serviceUser.AddHashPassword(customer.User);
        
        _unitOfWork.CustomerRepository.Create(customer);
        await _unitOfWork.CommitAsync();

        await _mediator.Publish(
            new CustumerAuthNotification(customer,"Conta criada!",
                MenssagesEmail.RegisterCustomerMenssage(customer.User.Email.Address)));
        
        return Result<string>.Sucess(GenerateJwtTokenOfCustomer(customer));
    }

    private async Task<bool> CustomerExisting(string name)
        => await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Name == name) is not null;

}