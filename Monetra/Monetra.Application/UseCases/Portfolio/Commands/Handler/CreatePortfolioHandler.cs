using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.Commum.Abstraction;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class CreatePortfolioHandler: HandlerBase, IRequestHandler<CreatePortfolioRequest,Result>
{
    public CreatePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public async Task<Result> Handle(CreatePortfolioRequest request, CancellationToken cancellationToken)
    {
        var custumer = await _unitOfWork.CustomerRepository.
            GetByPredicate(x => x.Id == Guid.Parse(request.Model.Userid)); 
        if (custumer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        custumer.Portfolio = request.Model.Portfolio;
         _unitOfWork.CustomerRepository.Update(custumer);
         _unitOfWork.PortfolioRepository.Create(request.Model.Portfolio);
         await _unitOfWork.CommitAsync();
         return Result.Success();
    }
}