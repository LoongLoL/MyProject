using System;
using System.Collections.Generic;
using System.Text;
using NewsPublish.Models.Entitys;

namespace NewsPublish.Models.Response
{
    public class NewsClassifyModel : Entity
    {
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}
