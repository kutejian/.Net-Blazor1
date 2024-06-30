using Microsoft.AspNetCore.Mvc;
using IdentityServer.Models.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Authorization
{
    public interface IAuthorizationMethod
    {
        //授权方法
        public string Authorize();
        //回调方法
        public Task<ResultModel> Callback(string code, string state);
        //获取用户数据    

    }
}
