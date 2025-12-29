using MediatR;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetValueResultOfMonthHandler : HandlerBase , 
    IRequestHandler<GetValueResultOfMonthRequest,Result<IEnumerable<Transaction>>>
{
    public GetValueResultOfMonthHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async  Task<Result<IEnumerable<Transaction>>> Handle(GetValueResultOfMonthRequest request, CancellationToken cancellationToken)
    {
        var ports = await _unitOfWork.PortfolioRepository.
            GetPortfolioWithTransactionFromCustumer(request.IdCustomer);
        var portfolio = ports.FirstOrDefault(x=>x.Id == request.IdPortfolio);
        if (portfolio is null || !IdCustomerIsEquals(portfolio, request.IdCustomer))
            return Result<IEnumerable<Transaction>>.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);

        var startOfLastMonth = DateTime.Now.AddMonths(-1);
        return Result<IEnumerable<Transaction>>.Sucess(
            GetTransactionByDateRange(startOfLastMonth, DateTime.Now,portfolio));
    }

    private bool IdCustomerIsEquals(Domain.BackOffice.Entities.Portfolio port , Guid idCustomer)
        => port.CustomerId == idCustomer;

    private IEnumerable<Transaction> GetTransactionByDateRange(DateTime dateStart, DateTime dateFinal,
        Domain.BackOffice.Entities.Portfolio port)
    {
        return port
            .Transactions
            .Where(t => t.CreatedAt >= dateStart && t.CreatedAt <= dateFinal);
    }
}