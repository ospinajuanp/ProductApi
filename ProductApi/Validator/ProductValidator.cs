// ProductValidator.cs
using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validator;

public sealed class ProductValidator : AbstractValidator<Product> // <-- Cambia el nombre
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("Máximo 100 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que cero");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("La cantidad no puede ser negativa")
            .Must(q => q % 1 == 0).WithMessage("La cantidad debe ser un número entero");
    }
}
