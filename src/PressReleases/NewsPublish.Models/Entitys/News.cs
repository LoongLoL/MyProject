using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsPublish.Models.Entitys
{

    public class News : Entity
    {
        public News()
        {
            this.NewsComment = new HashSet<NewsComment>();
        }
        public int NewsClassifyId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        public string Contents { get; set; }
        public DateTime PublishTime { get; set; }
        [MaxLength(200)]
        public string Remark { get; set; }
        public virtual NewsClassify NewsClassify { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }

    }
}
