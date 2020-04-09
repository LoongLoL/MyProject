using NewsPublish.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Models.Response
{
    public class NewsCommentModel : Entity
    {
        public string NewsTitle { get; set; }
        public string Contents { get; set; }
        public DateTime AddTime { get; set; }
        public string Remark { get; set; }
        public string Floor { get; set; }
    }
}
