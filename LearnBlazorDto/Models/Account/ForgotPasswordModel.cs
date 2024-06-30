using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorDto.Models.Account
{
    public class ForgotPasswordModel
    {
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请填写邮箱")]
        [EmailAddress(ErrorMessage = "请填写正确的邮箱")]
        public string Email { get; set; }
    }
}
