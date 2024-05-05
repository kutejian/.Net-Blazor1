﻿using AntDesign;
using LearnBlazorDto.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorServerMediator.CategoryMediator
{

    public class CategoryOperationResponse
    {
        public string? Message { get; set; }
        public bool Result { get; set; }
    }

    //添加分类
    public class CategoryAdd : IRequest<CategoryOperationResponse>
    {
        public Category _category { get; }

        public CategoryAdd(Category category)
        {
            _category = category;
        }
    }
    
    //删除分类
    public class CategoryDelete : IRequest<CategoryOperationResponse>
    {
        public int Categoryid { get; }

        public CategoryDelete(int categoryid)
        {
            Categoryid = categoryid;
        }
        
    }

    //好像是获取分类
    public class CategoryGetMBXList : IRequest<List<string>>
    {
        public int _categoryid { get; }

        public CategoryGetMBXList(int categoryid)
        {
            _categoryid = categoryid;
        }
    }

    //获取单个分类
    public class CategoryGetModel : IRequest<Category>
    {
        public int _categoryid { get; }

        public CategoryGetModel(int categoryid)
        {
            _categoryid = categoryid;
        }
    }

    //获取树状图分类
    public class CategoryGetTreeModel : IRequest<List<Category>>
    {
    }

    //修改分类
    public class CategoryUpdate : IRequest<CategoryOperationResponse>
    {
        public Category _category { get; }

        public CategoryUpdate(Category category)
        {
            _category = category;
        }
    }
}