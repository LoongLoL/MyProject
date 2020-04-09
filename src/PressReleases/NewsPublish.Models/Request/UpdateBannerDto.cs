using System.ComponentModel.DataAnnotations;
using NewsPublish.Models.Entitys;

namespace NewsPublish.Models.Request
{
    public class UpdateBannerDto : Entity
    {
        [MaxLength(200)]
        public string Image { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }
    }
}
