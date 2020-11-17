using FluentValidation;
using HealthStore.Models.Contracts.Requests;

namespace HealthStore.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MaximumLength(35);
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}
