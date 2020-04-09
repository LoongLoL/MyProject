using System;
using System.Collections.Generic;
using System.Text;
using NewsPublish.Models.Entitys;

namespace NewsPublish.Models.Request
{
    public class UpdateNewsClassifyDto : Entity
    {
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}
