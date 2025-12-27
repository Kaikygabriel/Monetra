using MediatR;
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
            return Result.Failure(new("Portfolio.NoExisting", "Portfolio no existing"));
        if(await UserIsEquals(request.Model.Userid,request.Model.Portfolio.Id))
            return Result.Failure(new("UserId.IsNotEqual", "UserId of request Is Not Equal user id from portfolio"));

        _unitOfWork.PortfolioRepository.Update(request.Model.Portfolio);
        _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private async Task<bool> PortfolioExisting(Guid id)
        => _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == id) is not null;

    private async Task<bool> UserIsEquals(Guid idUser, Guid IdPortfloio)
    {
        var portifolio =await  _unitOfWork.CustomerRepository.GetPortfolioFromCustumer(idUser);
        if (portifolio is null || !portifolio.Id.Equals(IdPortfloio))
            return false;
        return true;
    }
}