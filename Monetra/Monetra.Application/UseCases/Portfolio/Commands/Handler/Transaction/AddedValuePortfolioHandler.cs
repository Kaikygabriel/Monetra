using MediatR;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler.Transaction;

public class AddedValuePortfolioHandler : HandlerBase, IRequestHandler<AddedValuePortfolioRequest,Result>
{
    private readonly IMediator _mediator;
    public AddedValuePortfolioHandler(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
    {
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddedValuePortfolioRequest request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.IdPortfolio);
        
        if (portfolio is null || request.Value <= 0)
            return Result.Failure(Errors.PortfolioNoExisting);
        
        if(!CustomerIdIsEquals(portfolio,request.CustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        
        var resultAddValue = portfolio.AddValue(request.Value,request.Type);
        if (!resultAddValue.IsSuccess)
            return Result.Failure(resultAddValue.Error);
        
        var result = await _mediator.Send(new AlterPercentageOfMarkRequest(request.CustomerId, request.Value));
        if (!result.IsSuccess)
           return result;
        
        _unitOfWork.TransactionRepository.Create(portfolio.Transactions.Last());
        _unitOfWork.PortfolioRepository.Update(portfolio);
        await _unitOfWork.CommitAsync();
 
        return Result.Success();
    }

    private bool CustomerIdIsEquals(Domain.BackOffice.Entities.Portfolio port, Guid idCustomer)
        => port.CustomerId == idCustomer;
}