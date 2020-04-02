using System;
using 仓储模式和依赖注入.Models.Models;

namespace 仓储模式和依赖注入.IServices
{
    public interface ISysUserInfoServices
    {
        SysUserInfo GetUserInfo(string name, string password);
    }
}
