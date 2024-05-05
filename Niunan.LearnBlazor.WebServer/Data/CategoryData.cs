using LearnBlazorDto.Models;
using LearnBlazorServerMediator.CategoryMediator;
using MediatR;

namespace Niunan.LearnBlazor.WebServer.Data
{
    public class CategoryData
    {
        public IMediator _mediator;
        public CategoryData(IMediator mediator)
        {
            _mediator = mediator;
        }


        public CategoryOperationResponse CategoryDataAdd(Category category)
        {
            var Addresult = _mediator.Send(new CategoryAdd(category)).Result;

            return Addresult;
        }

        public CategoryOperationResponse CategoryDataDelete(int CategoryId)
        {
            if (CategoryId <= 0)
            {
                return new CategoryOperationResponse() { Message = "操作失败", Result = false };
            }
            var DeResult = _mediator.Send(new CategoryDelete(CategoryId)).Result;

            return DeResult;
        }

        public List<string> CategoryDataGetMBXList(int CategoryId)
        {

            var GetMBXListResult = _mediator.Send(new CategoryGetMBXList(CategoryId)).Result;


            return GetMBXListResult;
        }

        public Category CategoryDataGetModel(int CategoryId)
        {
            if(CategoryId <= 0)
            {             
                throw new  Exception("不许为空"); 
            }

            Category category = _mediator.Send(new CategoryGetModel(CategoryId)).Result;

            return category;
        }

        public List<Category> CategoryDataGetTreeModel()
        {
            return _mediator.Send(new CategoryGetTreeModel()).Result;
        }

        public CategoryOperationResponse CategoryDataGetTreeUpdate(Category category)
        {
            var DeResult = _mediator.Send(new CategoryUpdate(category)).Result;

            return DeResult;
        }
    }
}
