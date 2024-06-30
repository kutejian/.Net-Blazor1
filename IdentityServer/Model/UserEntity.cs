using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Model
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // 添加一个新的属性来存储用户的路径
        public string UserPath { get; set; }
        // 添加一个新的属性来存储用户头像的 URL
        public string AvatarUrl { get; set; }

        // 添加一个新的属性来存储用户注册时间
        public DateTime RegistrationTimestamp { get; set; }
    }
}
