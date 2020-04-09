using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Models.Request
{
    public class AddNewsDto
    {
        public int NewsClassifyId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public string Remark { get; set; }
    }
}
