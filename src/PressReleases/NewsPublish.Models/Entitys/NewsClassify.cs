using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPublish.Models.Entitys
{
    public class NewsClassify : Entity
    {
        public NewsClassify()
        {
            this.News = new HashSet<News>();
        }
        [MaxLength(100)]
        public string Name { get; set; }
        public int Sort { get; set; }
        [MaxLength(200)]
        public string Remark { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
