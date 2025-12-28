using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Application.UseCases.Portfolio.Query.Response;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetPortfolioByIdUserHandler : HandlerBase,
    IRequestHandler<GetPortfolioByCustumerIdRequest,Result<IEnumerable<PotfolioGetByUserReponse>>>
{
    public GetPortfolioByIdUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<IEnumerable<PotfolioGetByUserReponse>>> Handle(GetPortfolioByCustumerIdRequest request, CancellationToken cancellationToken)
    {
        var portfolios = await _unitOfWork.CustomerRepository.GetPortfolioFromCustumer(request.Id);
        var portfoliosResponse = PotfolioGetByUserReponse.ConvertListInResponse(portfolios);
        return portfolios is not null
            ? Result<IEnumerable<PotfolioGetByUserReponse>>.Sucess(portfoliosResponse)
            : Result<IEnumerable<PotfolioGetByUserReponse>>.Failure(new Error("Portfolio.NoExisting","Portfolio No Existing"));
    }
}