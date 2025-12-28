using MediatR;
using Microsoft.AspNetCore.Identity;
using Monetra.Application.Commum;
using Monetra.Application.Commum.Abstraction;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class UpdatePortfolioHandler : HandlerBase , IRequestHandler<UpdatePortfolioRequest,Result>
{
    public UpdatePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(UpdatePortfolioRequest request, CancellationToken cancellationToken)
    {
        if(await PortfolioExisting(request.Model.Portfolio.Id))
            return Result.Failure(Errors.PortfolioNoExisting);
        if(await CustomerIsEquals(request.Model.CustomerId,request.Model.Portfolio.Id))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);

        _unitOfWork.PortfolioRepository.Update(request.Model.Portfolio);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private async Task<bool> PortfolioExisting(Guid id)
        => await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == id) is null;

    private async Task<bool> CustomerIsEquals(Guid customerId, Guid IdPortfloio)
    {
        var portfolios =await  _unitOfWork.CustomerRepository.GetPortfolioFromCustumer(customerId);
        var portfolio = portfolios.FirstOrDefault(x=>x.Id == IdPortfloio);
        if (portfolio is null)
            return false;
        return true;
    }
}