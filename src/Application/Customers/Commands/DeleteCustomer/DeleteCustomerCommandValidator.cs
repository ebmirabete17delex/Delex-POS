using Delex_POS.Application.Common.Interfaces.Repositories.Customer;

namespace Delex_POS.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    public DeleteCustomerCommandValidator(ICustomerQueryRepository customerQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Customer Id is required.")
            .NotNull().WithMessage("Customer Id is required.")
            .MustAsync(CustomerExists).WithMessage("A valid customer Id is required.");
    }
    private async Task<bool> CustomerExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _customerQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
