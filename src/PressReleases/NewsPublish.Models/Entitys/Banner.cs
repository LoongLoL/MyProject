using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPublish.Models.Entitys
{
    public class Banner : Entity
    {
        [MaxLength(200)]
        public string Image { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        [MaxLength(200)]
        public string Remark { get; set; }
    }
}
