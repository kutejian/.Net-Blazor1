using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorDto.Models.Account
{
    public class ResetPasswordModel
    {
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, ErrorMessage = "密碼長度必須在 {2} 到 {1} 個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "确认密码不能为空")]
        [StringLength(100, ErrorMessage = "密碼長度必須在 {2} 到 {1} 個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
    }
}
