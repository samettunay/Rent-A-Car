using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(10);
            RuleFor(c => c.DailyPrice).NotEmpty();
            //RuleFor(c => c.DailyPrice).GreaterThan(2);
            //RuleFor(c => c.DailyPrice).GreaterThan(10).When(c => c.BrandId == 1);
            //RuleFor(c => c.Description).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı!");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
