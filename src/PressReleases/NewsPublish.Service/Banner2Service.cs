using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsPublish.Models.Entitys;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;

namespace NewsPublish.Service
{
    public class Banner2Service

    {
        private readonly IBannerRepository _bannerRepository;

        public Banner2Service(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        /// <summary>
        /// 增加banner
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel AddBanner(AddBannerDto dto)
        {
            var banner = new Banner { AddTime = DateTime.Now, Remark = dto.Remark, Image = dto.Image, Url = dto.Url };
            _bannerRepository.Add(banner);
            if (_bannerRepository.SaveChanges() > 0)
                return new ResponseModel() { Code = 200, Result = "banner添加成功！" };
            return new ResponseModel() { Code = 0, Result = "banner添加失败！" };
        }

        /// <summary>
        /// 获取banner列表
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetBannerList()
        {
            var bannerList = _bannerRepository.GetAll().OrderByDescending(c => c.AddTime);
            var result = new ResponseModel { Code = 200, Result = "Banners获取成功！", Data = new List<BannerModel>() };
            foreach (var banner in bannerList)
            {
                result.Data.Add(new BannerModel
                {
                    Id = banner.Id,
                    Image = banner.Image,
                    AddTime = banner.AddTime,
                    Remark = banner.Remark,
                    Url = banner.Url
                });
            }
            return result;
        }


        /// <summary>
        /// 删除benner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel DeleteBanne(int id)
        {
            var banner = _bannerRepository.GetById(id);
            if (banner == null)
                return new ResponseModel { Code = 0, Result = $"不存在Id为{id}的数据！" };
            _bannerRepository.Remove(id);
            var i = _bannerRepository.SaveChanges();
            if (i > 0)
                return new ResponseModel { Code = 200, Result = $"删除Id为{id}的数据成功！" };
            return new ResponseModel { Code = 0, Result = $"删除Id为{id}的数据失败！" };
        }

        /// <summary>
        /// 更新banner
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseModel UpdateBanner(UpdateBannerDto dto)
        {
            var banner = _bannerRepository.GetById(dto.Id);
            if (banner == null)
                return new ResponseModel { Code = 0, Result = $"不存在Id为{dto.Id}的数据！" };
            banner.Remark = dto.Remark;
            banner.Image = dto.Image;
            banner.Url = dto.Url;
            _bannerRepository.Update(banner);
            var i = _bannerRepository.SaveChanges();
            if (i > 0)
                return new ResponseModel() { Code = 200, Result = "banner修改成功！" };
            return new ResponseModel() { Code = 0, Result = "banner修改失败！" };
        }
    }
}
