using NewsPublish.Models.Entitys;

namespace NewsPublish.Models.Request
{
    public class AddNewsClassifyDto : Entity
    {
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}
