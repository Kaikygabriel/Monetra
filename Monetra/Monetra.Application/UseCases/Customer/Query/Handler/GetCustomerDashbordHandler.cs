using MediatR;
using Microsoft.AspNetCore.Identity;
using Monetra.Application.DTOs.Custumer;
using Monetra.Application.UseCases.Customer.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Customer.Query.Handler;

public class GetCustomerDashbordHandler : HandlerBase,
    IRequestHandler<GetCustomerDashbordRequest,Result<CustomerDashboardDTO>>
{
    public GetCustomerDashbordHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<CustomerDashboardDTO>> Handle(GetCustomerDashbordRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByPredicateWithUser(x => x.Id == request.CustomerId);
        if (customer is null)
            return Result<CustomerDashboardDTO>.Failure(Errors.CustumerNoExisting);
        var portfolios = await _unitOfWork.PortfolioRepository.GetPortfolioFromCustumer(customer.Id);
        //var mark = await _unitOfWork.MarkRepository.GetByPredicate(x => x.CustomerId == request.CustomerId);
            
        var result = new CustomerDashboardDTO(customer.Name, customer.User.Email.Address, portfolios, null);
        return Result<CustomerDashboardDTO>.Success(result);
    }
}