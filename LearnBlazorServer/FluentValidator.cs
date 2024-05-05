using FluentValidation;
using LearnBlazorServerMediator.CategoryMediator;
using LearnBlazorServerMediator.ProductMediator;
using NetTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorRepository
{
    public class FluentValidatorCategory<Entity>
    {
        private readonly IValidator<Entity> _validation;

        public FluentValidatorCategory(IValidator<Entity> validation)
        {
            _validation = validation;
        }

        public Task<CategoryOperationResponse> ValidatorUtility(Entity entity)
        {
            var validationResult = _validation.Validate(entity);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Task.FromResult(new CategoryOperationResponse() {  Message = errors.First() ,Result = validationResult.IsValid } );
            }
            return Task.FromResult(new CategoryOperationResponse() { Message = "操作成功", Result = validationResult.IsValid });
        }
    }
    public class FluentValidatorProduct<Entity>
    {
        private readonly IValidator<Entity> _validation;

        public FluentValidatorProduct(IValidator<Entity> validation)
        {
            _validation = validation;
        }

        public Task<ProductOperationResponse> ValidatorUtility(Entity entity)
        {
            var validationResult = _validation.Validate(entity);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Task.FromResult(new ProductOperationResponse() { Message = errors.First(), Result = validationResult.IsValid });
            }
            return Task.FromResult(new ProductOperationResponse() { Message = "操作成功", Result = validationResult.IsValid });
        }
    }
}