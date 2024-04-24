using FluentValidation;
using LearnBlazorDto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorDto.ModelsFluentValidation
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("不能为空");
            /*            RuleFor(x => x.).NotEmpty().EmailAddress().WithMessage("邮箱地址不合法");
                        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("密码至少6位");*/
        }
    }
}