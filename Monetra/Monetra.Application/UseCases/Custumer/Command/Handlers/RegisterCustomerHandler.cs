using MediatR;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases.Custumer.Command.Request;
using Monetra.Application.UseCases.Custumer.Notification;
using Monetra.Application.UseCases.Custumer.Notification.Request;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Custumer.Command.Handlers;

public class RegisterCustomerHandler:HandlerAuthBase, IRequestHandler<RegisterCustumerRequest,string?>
{
    private readonly IServiceUser _serviceUser;
    private readonly IMediator _mediator;
    public RegisterCustomerHandler(IUnitOfWork unitOfWork, IMediator mediator, IServiceUser serviceUser,ITokenService tokenService) 
        : base(unitOfWork,tokenService)
    {
        _mediator = mediator;
        _serviceUser = serviceUser;
    }

    public async Task<string?> Handle(RegisterCustumerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await CreateCustomer(request);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private async Task<string> CreateCustomer(RegisterCustumerRequest request)
    {
        if (await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Name == request.Model.Name) is not null)
            return null;
        var resultCreateUser = await _serviceUser.TryCreateAsync(request.Model.UserDto);
        if (!resultCreateUser)
            return null;
        Customer customer = request.Model;
        _unitOfWork.CustomerRepository.Create(customer);
        await _unitOfWork.CommitAsync();

        _mediator.Publish(new RegisterCustomerNotification(CreateMenssageOfEmail(customer)));
        
        return  GenerateJwtTokenOfCustomer(customer);
    }

    private EmailSending CreateMenssageOfEmail(Customer customer)
    {
        return
            new EmailSending("Conta criada",
                customer.User.Email.Address,
                customer.Name,
                MenssagesEmail.RegisterCustomerMenssage(customer.User.Email.Address));
    }
}