using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Models.Request
{
    public class AddCommentDto
    {
        public int NewsId { get; set; }
        public string Contents { get; set; }
    }
}
