using LearnBlazorDto.Models;
using LearnBlazorRepository;
using LearnBlazorRepository.Repository.Interface;
using MediatR;

namespace LearnBlazorServerMediator.CategoryMediator
{
    //添加产品

    public class CategoryHandlerAdd : IRequestHandler<CategoryAdd, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly FluentValidator<Category> _fluentValidator;

        public CategoryHandlerAdd(ICategoryRepository categoryRepository, FluentValidator<Category> fluentValidator)
        {
            _categoryRepository = categoryRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<string> Handle(CategoryAdd request, CancellationToken cancellationToken)
        {
            var result = _fluentValidator.ValidatorUtility(request._category).Result;
            if (result == "")
            {
                _categoryRepository.Add(request._category);
                return Task.FromResult("成功");
            }
            return Task.FromResult(result);
        }
    }

    public class CategoryHandlerDelete : IRequestHandler<CategoryDelete, string>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandlerDelete(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<string> Handle(CategoryDelete request, CancellationToken cancellationToken)
        {
            _categoryRepository.Delete(request._categoryid);
            return Task.FromResult("删除成功");
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

    public class CategoryHandlerUpdate : IRequestHandler<CategoryUpdate, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly FluentValidator<Category> _fluentValidator;

        public CategoryHandlerUpdate(ICategoryRepository categoryRepository, FluentValidator<Category> fluentValidator)
        {
            _categoryRepository = categoryRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<string> Handle(CategoryUpdate request, CancellationToken cancellationToken)
        {
            var result = _fluentValidator.ValidatorUtility(request._category).Result;

            if (result == "")
            {
                _categoryRepository.Update(request._category);
                return Task.FromResult("修改成功");
            }
            return Task.FromResult(result);
        }
    }
}