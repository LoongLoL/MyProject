using Microsoft.EntityFrameworkCore;
using NewsPublish.Models;
using NewsPublish.Models.Entitys;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsPublish.Service
{

    public class NewsCommentService
    {
        MyDbContext _db;
        NewsService _newsService;
        public NewsCommentService(MyDbContext db, NewsService newsService)
        {
            _db = db;
            _newsService = newsService;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel AddNewsComment(AddCommentDto dto)
        {
            var news = _newsService.GetNews(dto.NewsId);
            if (news == null)
                return new ResponseModel { Code = 0, Result = "新闻不存在！" };
            var com = new NewsComment { AddTime = DateTime.Now, NewsId = dto.NewsId, Contents = dto.Contents };
            _db.NewsComments.Add(com);
            var i = _db.SaveChanges();
            if (i > 0)
            {
                return new ResponseModel
                {
                    Code = 200,
                    Result = "新闻评论添加成功！",
                    Data = new NewsCommentModel
                    {
                        Contents = com.Contents,
                        Floor = $"#{news.Data.CommentCount + 1}",
                        AddTime = com.AddTime
                    }

                };

            }
            return new ResponseModel { Code = 0, Result = "新闻评论添加失败！" };
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel DeleteComment(int id)
        {
            var comment = _db.NewsComments.Find(id);
            if (comment == null)
                return new ResponseModel { Code = 0, Result = "评论不存在！" };
            _db.NewsComments.Remove(comment);
            int i = _db.SaveChanges();
            if (i > 0)
                return new ResponseModel { Code = 200, Result = "评论删除成功！" };
            return new ResponseModel { Code = 0, Result = "评论删除失败！" };
        }

        /// <summary>
        /// 获取评论集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public ResponseModel GetNewCommentList(Expression<Func<NewsComment, bool>> where)
        {
            var commentList = _db.NewsComments.Include("News").Where(where).OrderBy(c => c.AddTime);
            var response = new ResponseModel
            {
                Code = 200,
                Result = "评论获取成功！",
                Data = new List<NewsCommentModel>()
            };
            int floor = 1;
            foreach (var item in commentList)
            {
                response.Data.Add(new NewsCommentModel
                {
                    Id = item.Id,
                    NewsTitle = item.News.Title,
                    Contents = item.Contents,
                    AddTime = item.AddTime,
                    Remark = item.Remark,
                    Floor = $"#{floor}"
                });
                floor++;
            }
            response.Data.Reverse();
            return response;
        }
    }
}
