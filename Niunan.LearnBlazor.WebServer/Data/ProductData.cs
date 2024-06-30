using LearnBlazorDto.Models;
using LearnBlazorServerMediator.CategoryMediator;
using LearnBlazorServerMediator.ProductMediator;
using MediatR;

namespace Niunan.LearnBlazor.WebServer.Data
{
    public class ProductData
    {
        public IMediator _mediator;
        //定时器任务
        public TaskSchedulerQuartz.SchedulerService _schedulerService;
        public ProductData(IMediator mediator , TaskSchedulerQuartz.SchedulerService schedulerService)
        {
            _mediator = mediator;
            _schedulerService = schedulerService;
            //_schedulerService.RefreshIeaderboardRegularlyJobScheduleJob();
        }


        public ProductOperationResponse ProductDataAdd(Product category)
        {
            var AddResult = _mediator.Send(new ProductAdd(category)).Result;

            return AddResult;
        }

        public int ProductDataCalcCount(int CaId)
        {

            var CalcCountResult = _mediator.Send(new ProductCalcCount(CaId)).Result;

            return CalcCountResult;
        }

        public int ProductDataCalcCountPage(string searchKey = "", int caId = 0)
        {
            var CalcCountPageResult = _mediator.Send(new ProductCalcCountPage(searchKey,caId)).Result;

            return CalcCountPageResult;
        }

        public ProductOperationResponse ProductDataDelete(int productId)
        {
            if (productId <= 0)
            {
                return new ProductOperationResponse() { Message = "操作失败", Result = false };
            }
            var DeleteResult = _mediator.Send(new ProductDelete(productId)).Result;

            return DeleteResult;
        }

        public List<Product> ProductDataGetListPage(string searchKey = "", int caId = 0, int pageSize = 8, int pageIndex = 1)
        {
            return _mediator.Send(new ProductGetListPage(searchKey,caId,pageSize,pageIndex)).Result;
        }
        public Product ProductDataGetModel(int productId)
        {
            if (productId <= 0)
            {
                throw new Exception("不许为空");
            }
            var GetModelResult = _mediator.Send(new ProductGetModel(productId)).Result;

            return GetModelResult;
        }
        public ProductOperationResponse ProductDataUpdate(Product product)
        {

            var DataUpdateResult = _mediator.Send(new ProductUpdate(product)).Result;

            return DataUpdateResult;
        }
    }
}
