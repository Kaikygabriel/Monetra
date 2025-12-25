using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases;

public abstract class HandlerBase
{
    protected IUnitOfWork _unitOfWork;

    protected HandlerBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}