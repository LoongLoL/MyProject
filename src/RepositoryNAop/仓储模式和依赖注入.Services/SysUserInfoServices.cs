using System;
using 仓储模式和依赖注入.IRepository;
using 仓储模式和依赖注入.IServices;
using 仓储模式和依赖注入.Models.Models;
using 仓储模式和依赖注入.Repository;

namespace 仓储模式和依赖注入.Services
{
    public class SysUserInfoServices : ISysUserInfoServices
    {
        private ISysUserInfoRepository dal = new SysUserInfoRepository();
        public SysUserInfo GetUserInfo(string name, string password)
        {
            return dal.GetUserInfo(name, password);
        }
    }
}
