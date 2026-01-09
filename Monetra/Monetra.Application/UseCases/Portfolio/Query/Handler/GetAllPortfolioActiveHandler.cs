using MediatR;
using Monetra.Application.DTOs.Portfolio;
using Monetra.Application.UseCases.Portfolio.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Query.Handler;

public class GetAllPortfolioActiveHandler : HandlerBase,
        IRequestHandler<GetAllPortfolioActiveRequest,Result<IEnumerable<GetAllPortfolioActiveDTo>>>
{
        public GetAllPortfolioActiveHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Result<IEnumerable<GetAllPortfolioActiveDTo>>> Handle(GetAllPortfolioActiveRequest request, CancellationToken cancellationToken)
        {
            var portfolios = await _unitOfWork
                .PortfolioRepository
                .GetAllByVisible(request.Skip, request.Take);
            return Result<IEnumerable<GetAllPortfolioActiveDTo>>.Success(
                GetAllPortfolioActiveDTo.ToGetAllPortfolioActiveDTos(portfolios));
        }
}