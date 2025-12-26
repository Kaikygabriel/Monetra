using MediatR;
using Microsoft.AspNetCore.Identity;
using Monetra.Application.Commum;
using Monetra.Application.Service;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases.Custumer.Command.Request;
using Monetra.Application.UseCases.Custumer.Notification.Request;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Custumer.Command.Handlers;

public class RegisterCustomerHandler:HandlerCustomerBase, IRequestHandler<RegisterCustumerRequest,Result<string>>
{
    private readonly IMediator _mediator;
    public RegisterCustomerHandler(IUnitOfWork unitOfWork, IMediator mediator,ITokenService tokenService,IServiceUser  serviceUser) 
        : base(unitOfWork,tokenService, serviceUser)
    {
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(RegisterCustumerRequest request, CancellationToken cancellationToken)
    {
        return await CreateCustomer(request);
    }

    private async Task<Result<string>> CreateCustomer(RegisterCustumerRequest request)
    {
        if (await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Name == request.Model.Name) is not null)
            return Result<string>.Failure(Errors.InvalidEmail);
        
        var resultCreateUser = await _serviceUser.TryCreateAsync(request.Model.UserDto);
        if (!resultCreateUser)
            return Result<string>.Failure(Errors.UserCreatedFalid);
        
        Customer customer = request.Model;
        _unitOfWork.CustomerRepository.Create(customer);
        await _unitOfWork.CommitAsync();

        await _mediator.Publish(new CustumerAuthNotification(ServiceEmail.CreateMenssageOfEmail(customer,"Conta criada!")));
        
        return Result<string>.Sucess(GenerateJwtTokenOfCustomer(customer));
    }
}