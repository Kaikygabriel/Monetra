using MediatR;
using Monetra.Application.DTOs.Custumer;
using Monetra.Application.UseCases.Customer.Query.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Customer.Query.Handler;

public class GetCustomerDashbordHandler : HandlerBase,
    IRequestHandler<GetCustomerDashbordRequest,Result<CustomerDashboardDto>>
{
    public GetCustomerDashbordHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<CustomerDashboardDto>> Handle(GetCustomerDashbordRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByPredicateWithUserAndMarkAndExpense(x => x.Id == request.CustomerId);
        if (customer is null)
            return Result<CustomerDashboardDto>.Failure(Errors.CustumerNoExisting);
        var portfolios = await _unitOfWork.PortfolioRepository.GetPortfolioFromCustumer(customer.Id);

        var result = new CustomerDashboardDto(customer.Name, customer.User.Email.Address, customer.Mark, portfolios);
        
        return Result<CustomerDashboardDto>.Success(result);
    }
}