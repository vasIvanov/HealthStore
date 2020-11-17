using FluentValidation;
using HealthStore.Models.Contracts.Requests;
namespace HealthStore.Validators
{
    public class PlanRequestValidator : AbstractValidator<PlanRequest>
    {
        public PlanRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
