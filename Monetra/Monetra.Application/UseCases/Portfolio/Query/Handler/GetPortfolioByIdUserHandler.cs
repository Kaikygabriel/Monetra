using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetPortfolioByIdUserHandler : HandlerBase,
    IRequestHandler<GetPortfolioByCustumerIdRequest,Result<Domain.BackOffice.Entities.Portfolio>>
{
    public GetPortfolioByIdUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<Domain.BackOffice.Entities.Portfolio>> Handle(GetPortfolioByCustumerIdRequest request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.CustomerRepository.GetPortfolioFromCustumer(request.Id);
        return portfolio is not null
            ? Result<Domain.BackOffice.Entities.Portfolio>.Sucess(portfolio)
            : Result<Domain.BackOffice.Entities.Portfolio>.Failure(new Error("Portfolio.NoExisting","Portfolio No Existing"));
    }
}