using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SwaggerAndJwt.Helper;
using SwaggerAndJwt.Models;

namespace SwaggerAndJwt.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public ActionResult<string> GetToken(string name, string pwd)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
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
                token = jwtStr
            });
        }
    }
}
