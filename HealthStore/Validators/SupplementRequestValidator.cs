using FluentValidation;
using HealthStore.Models.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthStore.Validators
{
    public class SupplementRequestValidator : AbstractValidator<SupplementsRequest>
    {
        public SupplementRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MaximumLength(25);
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.SuitableDietId).GreaterThan(0);
        }
    }
}
