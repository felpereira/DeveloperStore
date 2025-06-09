using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    // Validator for the CreateSaleCommand.
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("O número da venda é obrigatório.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("A identidade do cliente é obrigatória.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("A identidade da filial é obrigatória.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

            RuleForEach(x => x.Items).SetValidator(new SaleItemCommandValidator());
        }
    }

    // Validator for the SaleItemCommand.
    public class SaleItemCommandValidator : AbstractValidator<SaleItemCommand>
    {
        public SaleItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("A identidade do produto é obrigatória.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("O preço unitário não pode ser negativo.");
        }
    }
}
