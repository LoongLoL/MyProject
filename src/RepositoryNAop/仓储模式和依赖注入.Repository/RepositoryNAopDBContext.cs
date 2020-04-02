using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using 仓储模式和依赖注入.Models.Models;

namespace 仓储模式和依赖注入.Repository
{
    public class RepositoryNAopDBContext : DbContext
    {
        public DbSet<SysUserInfo> SysUserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(AppSettings.app(new[] { "ConnectionStrings", "Default" }));
    }
}
