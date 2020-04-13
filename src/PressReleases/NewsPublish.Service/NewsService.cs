using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using Microsoft.EntityFrameworkCore;
using NewsPublish.Models;
using NewsPublish.Models.Entitys;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;

namespace NewsPublish.Service
{
    public class NewsService
    {
        MyDbContext _db;

        public NewsService(MyDbContext db)
        {
            this._db = db;
        }

        #region 新闻类别
        /// <summary>
        /// 添加新闻类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel AddNewsClassify(AddNewsClassifyDto dto)
        {
            var exit = _db.NewsClassifies.FirstOrDefault(c => c.Name == dto.Name) != null;
            if (exit)
                return new ResponseModel { Code = 0, Result = "该类别已经存在！" };
            var classify = new NewsClassify { Name = dto.Name, Sort = dto.Sort, Remark = dto.Remark };
            _db.NewsClassifies.Add(classify);
            int i = _db.SaveChanges();
            if (i > 0)
                return new ResponseModel { Code = 200, Result = "新闻类别添加成功！" };
            return new ResponseModel { Code = 0, Result = "新闻类别添加失败！" };
        }

        /// <summary>
        /// 获取新闻类别集合
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetNewsClassifyList()
        {
            var NewsClassifyList = _db.NewsClassifies.ToList().OrderByDescending(c => c.Sort);
            var result = new ResponseModel { Code = 200, Result = "新闻列别列表获取成功！", Data = new List<NewsClassifyModel>() };
            foreach (var item in NewsClassifyList)
            {
                result.Data.Add(new NewsClassifyModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Sort = item.Sort,
                    Remark = item.Remark
                });
            }
            return result;
        }


        /// <summary>
        /// 通过条件获取一个新闻类别数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        private NewsClassify GetOneNewsClassify(Expression<Func<NewsClassify, bool>> where)
        {
            return _db.NewsClassifies.FirstOrDefault(where);
        }

        /// <summary>
        /// 通过Id获取一个新闻类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel GetOneNewsClassify(int id)
        {
            var classify = _db.NewsClassifies.Find(id);
            if (classify == null)
                return new ResponseModel() { Code = 0, Result = $"不存在id为{id}的新闻类别！" };
            return new ResponseModel
            {
                Code = 200,
                Result = "新闻类别获取成功！",
                Data = new NewsClassifyModel
                {
                    Id = classify.Id,
                    Name = classify.Name,
                    Sort = classify.Sort,
                    Remark = classify.Remark,
                }
            };
        }

        /// <summary>
        /// 更新新闻类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel UpdateNewsClassify(UpdateNewsClassifyDto dto)
        {
            var classify = this.GetOneNewsClassify(c => c.Id == dto.Id);
            if (classify == null)
                return new ResponseModel { Code = 0, Result = $"不存在Id为{dto.Id}的数据！" };
            classify.Remark = dto.Remark;
            classify.Name = dto.Name;
            classify.Sort = dto.Sort;
            _db.NewsClassifies.Update(classify);
            var i = _db.SaveChanges();
            if (i > 0)
                return new ResponseModel() { Code = 200, Result = "新闻类别修改成功！" };
            return new ResponseModel() { Code = 0, Result = "新闻类别修改失败！" };
        }
        #endregion

        #region 新闻
        /// <summary>
        /// 增加新闻数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel AddNews(AddNewsDto dto)
        {
            var classify = this.GetOneNewsClassify(c => c.Id == dto.NewsClassifyId);
            if (classify == null)
                return new ResponseModel { Code = 0, Result = "该类别不存在！" };
            var news = new News
            {
                Title = dto.Title,
                Contents = dto.Contents,
                Image = dto.Image,
                NewsClassifyId = dto.NewsClassifyId,
                PublishTime = DateTime.Now,
                Remark = dto.Remark
            };

            _db.News.Add(news);
            int i = _db.SaveChanges();
            if (i > 0)
                return new ResponseModel { Code = 200, Result = "新闻添加成功！" };
            return new ResponseModel { Code = 0, Result = "新闻添加失败！" };
        }
        /// <summary>
        /// 通过Id删除新闻数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel DeleteNews(int id)
        {
            var news = _db.News.FirstOrDefault(c => c.Id == id);
            if (news == null)
                return new ResponseModel { Code = 0, Result = "新闻不存在！" };
            _db.News.Remove(news);
            int i = _db.SaveChanges();
            if (i > 0)
                return new ResponseModel { Code = 200, Result = "新闻删除成功！" };
            return new ResponseModel { Code = 0, Result = "新闻删除失败！" };
        }
        /// <summary>
        /// 获取一个新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel GetNews(int id)
        {
            var news = _db.News.Include("NewsClassify").Include("NewsComment").FirstOrDefault(c => c.Id == id);
            if (news == null)
                return new ResponseModel { Code = 0, Result = "新闻不存在！" };
            return new ResponseModel
            {
                Code = 200,
                Result = "新闻获取成功！",
                Data = new NewsModel
                {
                    Id = news.Id,
                    Contents = news.Contents,
                    Image = news.Image,
                    PublishTime = news.PublishTime.ToString("yyyy-MM-dd"),
                    Remark = news.Remark,
                    Title = news.Title,
                    CommentCount = news.NewsComment.Count,
                    NewsClassifyName = news.NewsClassify.Name
                }
            };
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">所获取的页数</param>
        /// <param name="total">总数</param>
        /// <param name="where"条件集合</param>
        /// <returns></returns>
        public ResponseModel GetNewsListPage(int pageSize, int pageIndex, out int total, List<Expression<Func<News, bool>>> where)
        {
            var list = _db.News.Include("NewsClassify").Include("NewsComment");
            foreach (var item in where)
            {
                list = list.Where(item);
            }
            total = list.Count();
            var pageData = list.OrderByDescending(c => c.PublishTime).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            var response = new ResponseModel { Code = 200, Result = "获取分页数据成功！", Data = new List<NewsModel>() };
            foreach (var news in pageData)
            {
                response.Data.Add(new NewsModel
                {
                    Id = news.Id,
                    Contents = news.Contents.Length > 50 ? news.Contents.Substring(0, 50) : news.Contents,
                    Image = news.Image,
                    PublishTime = news.PublishTime.ToString("yyyy-MM-dd"),
                    Remark = news.Remark,
                    Title = news.Title,
                    CommentCount = news.NewsComment.Count,
                    NewsClassifyName = news.NewsClassify.Name
                });
            }
            return response;
        }

        /// <summary>
        /// 根据条件获取新闻列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public ResponseModel GetNewsList(Expression<Func<News, bool>> where, int topCount)
        {
            var list = _db.News.Include("NewsClassify").Include("NewsComment").Where(where).OrderByDescending(c => c.PublishTime).Take(topCount);

            var response = new ResponseModel { Code = 200, Result = "获取列表数据成功！", Data = new List<NewsModel>() };
            foreach (var news in list)
            {
                response.Data.Add(new NewsModel
                {
                    Id = news.Id,
                    Contents = news.Contents.Length > 50 ? news.Contents.Substring(0, 50) : news.Contents,
                    Image = news.Image,
                    PublishTime = news.PublishTime.ToString("yyyy-MM-dd"),
                    Remark = news.Remark,
                    Title = news.Title,
                    CommentCount = news.NewsComment.Count,
                    NewsClassifyName = news.NewsClassify.Name
                });
            }
            return response;
        }

        /// <summary>
        /// 最新评论的新闻获取
        /// </summary>
        /// <param name="where"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public ResponseModel GetNewCommentNewsList(Expression<Func<News, bool>> where, int topCount)
        {
            var newIds = _db.NewsComments.OrderByDescending(c => c.AddTime).GroupBy(c => c.NewsId).Select(c => c.Key).Take(topCount).ToList();

            var list = _db.News.Include("NewsClassify").Include("NewsComment").Where(c => newIds.Any() && newIds.Contains(c.Id)).Where(where).OrderByDescending(c => c.PublishTime);

            var response = new ResponseModel { Code = 200, Result = "最新评论的新闻数据获取成功！", Data = new List<NewsModel>() };
            foreach (var news in list)
            {
                response.Data.Add(new NewsModel
                {
                    Id = news.Id,
                    Contents = news.Contents.Length > 50 ? news.Contents.Substring(0, 50) : news.Contents,
                    Image = news.Image,
                    PublishTime = news.PublishTime.ToString("yyyy-MM-dd"),
                    Remark = news.Remark,
                    Title = news.Title,
                    CommentCount = news.NewsComment.Count,
                    NewsClassifyName = news.NewsClassify.Name
                });
            }
            return response;
        }

        /// <summary>
        /// 搜索一个新闻
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public ResponseModel GetSearchOneNews(Expression<Func<News, bool>> where)
        {
            var news = _db.News.Where(where).FirstOrDefault();
            var response = new ResponseModel { Code = 200, Result = "" };
            if (news == null)
            {
                response.Result = "搜索不到相关新闻！";
                return response;
            }
            response.Result = "搜索新闻成功！";
            response.Data = news.Id;
            return response;
        }

        /// <summary>
        /// 获取新闻数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public ResponseModel GetNewsCount(Expression<Func<News, bool>> where)
        {
            var count = _db.News.Where(where).Count();
            return new ResponseModel { Code = 200, Result = "新闻数量获取成功！", Data = count };
        }

        /// <summary>
        /// 获取新闻的相关推荐
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public ResponseModel GetRecommendNewsList(int newsId)
        {
            var news = _db.News.Find(newsId);
            if (news == null)
                return new ResponseModel { Code = 0, Result = "新闻不存在！" };
            var newsList = _db.News.Include(nameof(NewsComment))
                .Where(c => c.NewsClassifyId == news.NewsClassifyId && c.Id != newsId)
                .OrderByDescending(c => c.PublishTime).OrderByDescending(c => c.NewsComment.Count)
                .Take(6)
                .ToList();

            var response = new ResponseModel { Code = 200, Result = "推荐新闻数据获取成功！", Data = new List<NewsModel>() };
            foreach (var item in newsList)
            {
                response.Data.Add(new NewsModel
                {
                    Id = item.Id,
                    Contents = item.Contents.Length > 50 ? item.Contents.Substring(0, 50) : item.Contents,
                    Image = item.Image,
                    PublishTime = item.PublishTime.ToString("yyyy-MM-dd"),
                    Remark = item.Remark,
                    Title = item.Title,
                    CommentCount = item.NewsComment.Count,
                    NewsClassifyName = item.NewsClassify.Name
                });
            }
            return response;
        }
        #endregion
    }
}
