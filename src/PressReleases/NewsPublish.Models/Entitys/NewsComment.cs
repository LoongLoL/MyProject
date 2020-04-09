using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPublish.Models.Entitys
{
    public class NewsComment : Entity
    {
        public int NewsId { get; set; }
        [MaxLength(200)]
        public string Contents { get; set; }
        public DateTime AddTime { get; set; }
        [MaxLength(200)]
        public string Remark { get; set; }
        public virtual News News { get; set; }
    }
}
