using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;
using NewsPublish.Service;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace NewsPublish.Web.Areas.Admin.Controllers
{
    public class BannerController : AdminControllerBase
    {
        BannerService _bannerService;
        IHostEnvironment _hostEnvironment;

        public BannerController(BannerService bannerService, IHostEnvironment hostEnvironment)
        {
            _bannerService = bannerService;
            _hostEnvironment = hostEnvironment;
        }
        // GET: Banner
        public ActionResult Index()
        {
            var bannerList = _bannerService.GetBannerList();
            return View(bannerList);
        }

        // GET: Banner/Details/5
        public ActionResult BannerAdd(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddBanner(AddBannerDto dto, IFormFile uploadFile)
        {
            if (uploadFile == null)
                return Json(new ResponseModel { Code = 0, Result = "请上传图片！" });

            var webRootPath = _hostEnvironment.ContentRootPath;
            var relativeDirPath = "BannerPic";
            var absolutePath = Path.Combine(webRootPath, relativeDirPath);

            var fileTypes = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            var extension = Path.GetExtension(uploadFile.FileName);
            if (!fileTypes.Contains(extension.ToLower()))
                return Json(new ResponseModel { Code = 0, Result = "图片格式有误！" });

            if (!Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);

            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
            var filePath = Path.Combine(absolutePath, fileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadFile.CopyToAsync(stream);
            }
            dto.Image = Path.Combine("/BannerPic/", fileName);
            return Json(_bannerService.AddBanner(dto));
        }

        [HttpDelete]
        public JsonResult DelBanner(int id)
        {
            if (id <= 0)
                return Json(new ResponseModel { Code = 0, Result = "参数错误！" });
            return Json(_bannerService.DeleteBanne(id));
        }
    }
}