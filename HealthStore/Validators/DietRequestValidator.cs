using FluentValidation;
using HealthStore.Models.Contracts.Requests;

namespace HealthStore.Validators
{
    public class DietRequestValidator : AbstractValidator<DietRequest>
    {
        public DietRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MaximumLength(25);
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.SuitablePlanId).GreaterThan(0);
        }
    }
}
