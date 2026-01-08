using MediatR;
using Monetra.Application.DTOs.Portfolio;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetValueResultOfMonthHandler : HandlerBase , 
    IRequestHandler<GetValueResultOfMonthRequest,Result<GetValueResultMonthDto>>
{
    public GetValueResultOfMonthHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<GetValueResultMonthDto>> Handle(GetValueResultOfMonthRequest request,
        CancellationToken cancellationToken)
    {
        var ports = await _unitOfWork.PortfolioRepository.GetPortfolioWithTransactionFromCustumer(request.IdCustomer);
        var portfolio = ports.FirstOrDefault(x => x.Id == request.IdPortfolio);
        if (portfolio is null || !IdCustomerIsEquals(portfolio, request.IdCustomer))
            return Result<GetValueResultMonthDto>.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);

        var startOfLastMonth = DateTime.Now.AddMonths(-1);

        var transactionsInMonth = GetTransactionByDateRange(startOfLastMonth, DateTime.Now, portfolio);
        var phrase = $"{GeneratePhraseByTransactionInMonth(transactionsInMonth)};{GeneratePhraseByResultIfNoRemove(transactionsInMonth)}";
        return Result<GetValueResultMonthDto>.Success(new (phrase,transactionsInMonth));
    }

    private bool IdCustomerIsEquals(Domain.BackOffice.Entities.Portfolio port , Guid idCustomer)
        => port.CustomerId == idCustomer;

    private IEnumerable<Transaction> GetTransactionByDateRange(DateTime dateStart, DateTime dateFinal,
        Domain.BackOffice.Entities.Portfolio port)
    {
        return port
            .Transactions
            .Where(t => t.CreatedAt >= dateStart );
    }

    private string GeneratePhraseByTransactionInMonth(IEnumerable<Transaction> transaction)
    {
        if (transaction is null) return string.Empty;
        var sumTransactions = transaction.Where(x=>x.Amount>0).Sum(x => x.Amount);
        return $"In three months you have {(sumTransactions * 3).ToString("C")}";
    }
    private string GeneratePhraseByResultIfNoRemove(IEnumerable<Transaction> transaction)
    {
        if (transaction is null) return string.Empty;
        var sumTransactions = transaction.Where(x=>x.Amount <= 0).Sum(x => x.Amount * -1);
        return $"You have an extra {sumTransactions.ToString("C")} if you don't remove yourself. ";
    }
}