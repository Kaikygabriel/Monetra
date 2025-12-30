using MediatR;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetPortfolioByIdUserHandler : HandlerBase,
    IRequestHandler<GetPortfolioByCustumerIdRequest,Result<IEnumerable<Domain.BackOffice.Entities.Portfolio>>>
{
    public GetPortfolioByIdUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<IEnumerable<Domain.BackOffice.Entities.Portfolio>>> Handle(GetPortfolioByCustumerIdRequest request, CancellationToken cancellationToken)
    {
        var portfolios = await _unitOfWork.PortfolioRepository.GetPortfolioFromCustumer(request.Id);
        return portfolios is not null
            ? Result<IEnumerable<Domain.BackOffice.Entities.Portfolio>>.Success(portfolios)
            : Result<IEnumerable<Domain.BackOffice.Entities.Portfolio>>.Failure(new Error("Portfolio.NoExisting","Portfolio No Existing"));
    }
}