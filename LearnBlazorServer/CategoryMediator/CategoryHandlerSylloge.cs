using LearnBlazorDto.Models;
using LearnBlazorRepository;
using LearnBlazorRepository.Repository.Interface;
using MediatR;

namespace LearnBlazorServerMediator.CategoryMediator
{
    //添加产品

    public class CategoryHandlerAdd : IRequestHandler<CategoryAdd, CategoryOperationResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly FluentValidatorCategory<Category> _fluentValidator;

        public CategoryHandlerAdd(ICategoryRepository categoryRepository, FluentValidatorCategory<Category> fluentValidator)
        {
            _categoryRepository = categoryRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<CategoryOperationResponse> Handle(CategoryAdd request, CancellationToken cancellationToken)
        {
            var result = _fluentValidator.ValidatorUtility(request._category).Result;
            if (result.Result)
            {
                _categoryRepository.Add(request._category);
            }

            return Task.FromResult(result);
        }

    }
    //删除
    public class CategoryHandlerDelete : IRequestHandler<CategoryDelete, CategoryOperationResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandlerDelete(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<CategoryOperationResponse> Handle(CategoryDelete request, CancellationToken cancellationToken)
        {
          
            _categoryRepository.Delete(request.Categoryid);
            return Task.FromResult(new CategoryOperationResponse() { Message = "操作成功", Result = true } );
        }
    }

    public class CategoryHandlerGetMBXList : IRequestHandler<CategoryGetMBXList, List<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandlerGetMBXList(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<string>> Handle(CategoryGetMBXList request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_categoryRepository.GetMBXList(request._categoryid));
        }
    }

    public class CategoryHandlerGetModel : IRequestHandler<CategoryGetModel, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandlerGetModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<Category> Handle(CategoryGetModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_categoryRepository.GetModel(request._categoryid));
        }
    }

    public class CategoryHandlerGetTreeModel : IRequestHandler<CategoryGetTreeModel, List<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandlerGetTreeModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<Category>> Handle(CategoryGetTreeModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_categoryRepository.GetTreeModel());
        }
    }

    public class CategoryHandlerUpdate : IRequestHandler<CategoryUpdate, CategoryOperationResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly FluentValidatorCategory<Category> _fluentValidator;

        public CategoryHandlerUpdate(ICategoryRepository categoryRepository, FluentValidatorCategory<Category> fluentValidator)
        {
            _categoryRepository = categoryRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<CategoryOperationResponse> Handle(CategoryUpdate request, CancellationToken cancellationToken)
        {
            var result = _fluentValidator.ValidatorUtility(request._category).Result;

            if (result.Result)
            {
                _categoryRepository.Update(request._category);
              
            }
            return Task.FromResult(result);
        }
    }
}