using AntDesign;
using LearnBlazorDto.Models;
using LearnBlazorRepository;
using LearnBlazorRepository.Repository.Interface;
using LearnBlazorServerMediator.CategoryMediator;
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

    public class ProductHandlerAdd : IRequestHandler<ProductAdd, ProductOperationResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly FluentValidatorProduct<Product> _fluentValidator;
        public ProductHandlerAdd(IProductRepository productRepository, FluentValidatorProduct<Product> fluentValidator)
        {
            _productRepository = productRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<ProductOperationResponse> Handle(ProductAdd request, CancellationToken cancellationToken)
        {

            var result = _fluentValidator.ValidatorUtility(request._product).Result;
            if (result.Result)
            {
                _productRepository.Add(request._product);
            }

            return Task.FromResult(result);
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
    public class ProductHandlerDelete : IRequestHandler<ProductDelete, ProductOperationResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly FluentValidatorProduct<Product> _fluentValidator;
        public ProductHandlerDelete(IProductRepository productRepository , FluentValidatorProduct<Product> fluentValidator)
        {
            _productRepository = productRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<ProductOperationResponse> Handle(ProductDelete request, CancellationToken cancellationToken)
        {

            _productRepository.Delete(request._productId);
            return Task.FromResult(new ProductOperationResponse() { Message = "操作成功", Result = true });
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
    public class ProductHandlerUpdate : IRequestHandler<ProductUpdate, ProductOperationResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly FluentValidatorProduct<Product> _fluentValidator;
        public ProductHandlerUpdate(IProductRepository productRepository, FluentValidatorProduct<Product> fluentValidator)
        {
            _productRepository = productRepository;
            _fluentValidator = fluentValidator;
        }

        public Task<ProductOperationResponse> Handle(ProductUpdate request, CancellationToken cancellationToken)
        {           
            var result = _fluentValidator.ValidatorUtility(request._product).Result;
            if (result.Result)
            {
                _productRepository.Update(request._product);
            }
            return Task.FromResult(result);
        }
    }
}