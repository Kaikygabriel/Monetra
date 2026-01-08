using MediatR;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Application.UseCases.Portfolio.Commands.Request.Transaction;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler.Transaction;

public class RemoveValuePortfolioHandler : HandlerBase,IRequestHandler<RemoveValuePortfolioRequest,Result>
{
    private readonly IMediator _mediator;
    public RemoveValuePortfolioHandler(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
    {
        _mediator = mediator;
    }

    public async Task<Result> Handle(RemoveValuePortfolioRequest request, CancellationToken cancellationToken)
    {
        var categoryResult = Category.Factories.Create(request.NameCategory);
        if (!categoryResult.IsSuccess)
            return Result.Failure(categoryResult.Error);

        var portfolio = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.IdPortfolio);
        
        if (portfolio is null || request.Value <= 0)
            return Result.Failure(Errors.PortfolioNoExisting);
        
        if(!CustomerIdIsEquals(portfolio,request.CustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        
        var resultRemoveValue = portfolio.RemoveValue(request.Value,request.Type,categoryResult.Value);
        if (!resultRemoveValue.IsSuccess)
            return Result.Failure(resultRemoveValue.Error);
        
        var resultMarkPercentage = await _mediator.Send(new AlterPercentageOfMarkRequest(request.CustomerId,-request.Value));
        if (!resultMarkPercentage.IsSuccess)
            return resultMarkPercentage;
        
        _unitOfWork.TransactionRepository.Create(portfolio.Transactions.Last());
        _unitOfWork.PortfolioRepository.Update(portfolio);
        
        await _unitOfWork.CommitAsync();
        return Result.Success();    
    }
    private bool CustomerIdIsEquals(Domain.BackOffice.Entities.Portfolio port, Guid idCustomer)
        => port.CustomerId == idCustomer;

}