using System.Security.Cryptography;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Monetra.Application.UseCases.Customer.Command.Request.AlterPassword;
using Monetra.Application.UseCases.Customer.Notification.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Customer.Command.Handlers;

public class AlterPasswordHandler : HandlerBase,
    IRequestHandler<SendEmailForAlterPasswordRequest,Result>,
    IRequestHandler<AlterPasswordCustomerRequest,Result>
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _cache;
    public AlterPasswordHandler(IUnitOfWork unitOfWork, IMemoryCache cache, IMediator mediator) : base(unitOfWork)
    {
        _cache = cache;
        _mediator = mediator;
    }

    public async Task<Result> Handle(SendEmailForAlterPasswordRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByEmail(request.Email);
        if (customer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        var token = GenerateToken();
        var cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(2),
            Priority = CacheItemPriority.Normal,
            Size = 1
        };
        await _mediator.Publish(new CustumerAuthNotification
            (customer,"Alterar senha",MenssagesEmail.MenssageOfAlterPassword(token)));
        _cache.Set(token, request.Email,cacheOptions);
        return Result.Success();
    }

    private string GenerateToken()
        => Guid.NewGuid().ToString("N").Substring(0, 7);

    public async Task<Result> Handle(AlterPasswordCustomerRequest request, CancellationToken cancellationToken)
    {
        var resultOfCache = _cache.TryGetValue(request.Token, out string Email);
        if(!resultOfCache || Email is null)
            return Result.Failure(new("Token.Invalid","Token is invalid"));
        var user = await _unitOfWork.UserRepository.GetByPredicate(x => x.Email.Address == Email);
        user.UpdatePassword(request.NewPassword);

        await _unitOfWork.CommitAsync();
        _cache.Remove(request.Token);
        return Result.Success();
    }
    
}