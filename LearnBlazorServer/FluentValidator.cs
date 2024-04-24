using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorRepository
{
    public class FluentValidator<Entity>
    {
        private readonly IValidator<Entity> _validation;

        public FluentValidator(IValidator<Entity> validation)
        {
            _validation = validation;
        }

        public Task<string> ValidatorUtility(Entity entity)
        {
            var validationResult = _validation.Validate(entity);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Task.FromResult(errors.First());
            }
            return Task.FromResult("");
        }
    }
}