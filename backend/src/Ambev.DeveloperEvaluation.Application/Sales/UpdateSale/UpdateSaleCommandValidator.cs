using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    // Validator for the UpdateSaleCommand.
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty().WithMessage("A identidade da venda é obrigatória.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemCommandValidator());
        }
    }

    // Validator for the UpdateSaleItemCommand.
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("A identidade do produto é obrigatória.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("O preço unitário não pode ser negativo.");
        }
    }
}
