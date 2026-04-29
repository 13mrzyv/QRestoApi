using Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Requests
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            // Masa nömrəsi yoxlanışı
            RuleFor(x => x.TableId)
                .NotEmpty().WithMessage("Masa nömrəsi boş ola bilməz.")
                .GreaterThan(0).WithMessage("Zəhmət olmasa düzgün masa nömrəsi seçin.");

            // Sifariş olunan məhsulların siyahısı yoxlanışı
            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("Məhsul düzgün seçilməyib.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Məhsulun miqdarı 0-dan çox olmalıdır.");
            });
        }
    }
}
