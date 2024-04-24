using AntDesign;
using LearnBlazorDto.Models;
using LearnBlazorRepository.Repository.Interface;
using MediatR;
using Niunan.LearnBlazor.WebServer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorServerMediator.ProductMediator
{
    //添加产品

    public class ProductHandlerAdd : IRequestHandler<ProductAdd>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerAdd(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task Handle(ProductAdd request, CancellationToken cancellationToken)
        {
            _productRepository.Add(request._product);
            return Task.CompletedTask;
        }
    }

    //产品总和
    public class ProductHandlerCalcCount : IRequestHandler<ProductCalcCount, int>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerCalcCount(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<int> Handle(ProductCalcCount request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.CalcCount(request._caid));
        }
    }

    //产品总和分页
    public class ProductHandlerCalcCountPage : IRequestHandler<ProductCalcCountPage, int>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerCalcCountPage(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<int> Handle(ProductCalcCountPage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.CalcCountPage(request._searchKey, request._caId));
        }
    }

    //删除产品
    public class ProductHandlerDelete : IRequestHandler<ProductDelete>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerDelete(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task Handle(ProductDelete request, CancellationToken cancellationToken)
        {
            _productRepository.Delete(request._productId);
            return Task.CompletedTask;
        }
    }

    //获取产品分页
    public class ProductHandlerGetListPage : IRequestHandler<ProductGetListPage, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerGetListPage(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<List<Product>> Handle(ProductGetListPage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetListPage(request._searchKey, request._caId, request._pageSize, request._pageIndex));
        }
    }

    //获取单个产品
    public class ProductHandlerGetModel : IRequestHandler<ProductGetModel, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerGetModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> Handle(ProductGetModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetModel(request._productId));
        }
    }

    //修改产品
    public class ProductHandlerUpdate : IRequestHandler<ProductUpdate>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandlerUpdate(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task Handle(ProductUpdate request, CancellationToken cancellationToken)
        {
            _productRepository.Update(request._product);
            return Task.CompletedTask;
        }
    }
}