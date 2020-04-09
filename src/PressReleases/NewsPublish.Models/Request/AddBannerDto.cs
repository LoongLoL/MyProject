using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPublish.Models.Request
{
    public class AddBannerDto
    {
        public string Image { get; set; }

        public string Url { get; set; }

        public string Remark { get; set; }
    }
}
