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
    public class ProductAdd : IRequest
    {
        public Product _product { get; }

        public ProductAdd(Product product)
        {
            _product = product;
        }
    }

    public class ProductCalcCount : IRequest<int>
    {
        public int _caid;

        public ProductCalcCount(int caid)
        {
            _caid = caid;
        }
    }

    public class ProductCalcCountPage : IRequest<int>
    {
        public string _searchKey;
        public int _caId;

        public ProductCalcCountPage(string searchKey = "", int caId = 0)
        {
            _searchKey = searchKey;
            _caId = caId;
        }
    }

    public class ProductDelete : IRequest
    {
        public int _productId;

        public ProductDelete(int productid)
        {
            _productId = productid;
        }
    }

    public class ProductGetListPage : IRequest<List<Product>>
    {
        public string _searchKey;
        public int _caId;
        public int _pageSize;
        public int _pageIndex;

        public ProductGetListPage(string searchKey = "", int caId = 0, int pageSize = 8, int pageIndex = 1)
        {
            _searchKey = searchKey;
            _caId = caId;
            _pageSize = pageSize;
            _pageIndex = pageIndex;
        }
    }

    public class ProductGetModel : IRequest<Product>
    {
        public int _productId;

        public ProductGetModel(int productId)
        {
            _productId = productId;
        }
    }

    public class ProductUpdate : IRequest
    {
        public Product _product;

        public ProductUpdate(Product product)
        {
            _product = product;
        }
    }
}