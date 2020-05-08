using System;
using System.Collections.Generic;
using System.Text;
using NewsPublish.Models;
using NewsPublish.Models.Entitys;

namespace NewsPublish.Service
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(MyDbContext context)
            : base(context)
        {

        }
    }
}
