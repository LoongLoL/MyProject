using Microsoft.EntityFrameworkCore;
using NewsPublish.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace NewsPublish.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseMySql("Server=myr.aaronoy.online;Database=newspublish;Uid=oyyl;Pwd=oyyl@2020;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });

        public DbSet<Banner> Banners { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsClassify> NewsClassifies { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
    }
}
