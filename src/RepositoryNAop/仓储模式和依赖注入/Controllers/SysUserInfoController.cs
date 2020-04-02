using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using 仓储模式和依赖注入.IServices;
using 仓储模式和依赖注入.Models.Models;
using 仓储模式和依赖注入.Services;

namespace 仓储模式和依赖注入.Controllers
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    [Route("api/userinfo")]
    public class SysUserInfoController : ControllerBase
    {
        /// <summary>
        /// 通过用户名和密码获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("getuserinfo")]
        public SysUserInfo GetUserInfo(string name, string password)
        {
            ISysUserInfoServices sysUserInfoServices = new SysUserInfoServices();

            return sysUserInfoServices.GetUserInfo(name, password);
        }
    }
}
