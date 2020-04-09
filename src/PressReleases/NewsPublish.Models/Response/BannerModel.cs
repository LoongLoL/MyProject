using NewsPublish.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Models.Response
{
    public class BannerModel : Entity
    {
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
    }
}
