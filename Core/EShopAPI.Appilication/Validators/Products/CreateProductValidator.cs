using EShopAPI.Appilication.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Appilication.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Products>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("You must write the name of product")
                .MaximumLength(150)
                .MinimumLength(2)
                .WithMessage("The name of product must be beetween 2 and 150");

            RuleFor(s => s.Stock)
                .NotNull()
                .NotEmpty()
                .WithMessage("Write the the number of product")
                .Must(s => s >= 0)
                .WithMessage("The information about stock must be written");

            RuleFor(s => s.Price)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("Write the the price of product")
                  .Must(s => s >= 0)
                  .WithMessage("The information about price must be written");
        }

    }
}
