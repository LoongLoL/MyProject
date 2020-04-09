using NewsPublish.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Models.Response
{
    public class NewsModel : Entity
    {
        //public int NewsClassifyId { get; set; }
        public string NewsClassifyName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public string PublishTime { get; set; }
        public int CommentCount { get; set; }
        public string Remark { get; set; }
    }
}
