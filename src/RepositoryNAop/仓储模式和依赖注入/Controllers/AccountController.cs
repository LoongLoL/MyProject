using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 仓储模式和依赖注入.Common;
using 仓储模式和依赖注入.Models;

namespace 仓储模式和依赖注入.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Post(string name, string password)
        {

            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return new JsonResult(new
                {
                    Status = false,
                    message = "用户名或密码不能为空"
                });
            }

            TokenModelJwt tokenModel = new TokenModelJwt();
            tokenModel.Uid = 1;
            tokenModel.Role = name;

            jwtStr = JwtHelper.IssueJwt(tokenModel);
            suc = true;
            return Ok(new
            {
                success = suc,
                token = jwtStr,
                msg = "成功"
            });
        }
    }
}
