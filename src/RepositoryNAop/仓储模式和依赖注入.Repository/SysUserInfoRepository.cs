using System;
using 仓储模式和依赖注入.IRepository;
using 仓储模式和依赖注入.Models.Models;

namespace 仓储模式和依赖注入.Repository
{
    public class SysUserInfoRepository : ISysUserInfoRepository
    {
        public SysUserInfo GetUserInfo(string name, string password)
        {
            return new SysUserInfo(name, password);
        }
    }
}
