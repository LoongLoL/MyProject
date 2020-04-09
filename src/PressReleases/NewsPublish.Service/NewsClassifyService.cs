using System;
using System.Linq;
using NewsPublish.Models;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;

namespace NewsPublish.Service
{
    public class NewsClassifyService
    {
        MyDbContext _db;

        public NewsClassifyService(MyDbContext db)
        {
            this._db = db;
        }

        public ResponseModel AddNewsClassify(AddNewsClassifyDto dto)
        {
            var exit = _db.NewsClassifies.FirstOrDefault(c => c.Name == dto.Name) != null;
        }
    }
}
