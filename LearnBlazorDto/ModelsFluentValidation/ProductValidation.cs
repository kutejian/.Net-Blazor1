using FluentValidation;
using LearnBlazorDto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorDto.ModelsFluentValidation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("不能为空，请填写正确的商品名称");
            RuleFor(x => x.ThumbnailImage).NotEmpty().WithMessage("至少给张预览图");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("请选择分类");
            RuleFor(x => x.Code).NotEmpty().WithMessage("条码不能为空");
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("不能为空，请填写归属什么品牌");

            RuleFor(x => x.Unit).NotEmpty().WithMessage("不能为空，请输入单位");
            //后续加个产品数量的属性
            //RuleFor(x => x.Unit).NotEmpty().Must(x => x % 1 == 0).WithMessage("只能是整数");
            RuleFor(x => x.UnitPrice).Must(x => x % 1 == 0 || Math.Abs(x % 1) > 0).WithMessage("不能为空，只能是整数或小数");
        }
    }
}
