using FluentValidation;
using HealthStore.Models.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthStore.Validators
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Name).MaximumLength(35);
            RuleFor(x => x.Salary).GreaterThan(0);
        }
    }
}
