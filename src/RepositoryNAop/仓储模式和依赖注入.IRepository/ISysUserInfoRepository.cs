using System;
using 仓储模式和依赖注入.Models.Models;

namespace 仓储模式和依赖注入.IRepository
{
    public interface ISysUserInfoRepository
    {
        SysUserInfo GetUserInfo(string name, string password);
    }
}
