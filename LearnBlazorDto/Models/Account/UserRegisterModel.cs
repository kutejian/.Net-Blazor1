﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorDto.Models.Account
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "請輸入你的姓")]
        [Display(Name = "姓")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "請輸入你的名")]
        [Display(Name = "名")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "請輸入Email")]
        [EmailAddress(ErrorMessage = "Email格式不正確")]
        public string Email { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(100, ErrorMessage = "密碼長度必須在 {2} 到 {1} 個字元", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]

        [Compare("Password", ErrorMessage = "密碼不一致")]
        [StringLength(100, ErrorMessage = "密碼長度必須在 {2} 到 {1} 個字元", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
