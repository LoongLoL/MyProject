using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAndJwt.Models
{
    public class WeatherPostDto
    {
        /// <summary>
        /// 天气Id
        /// </summary>
        public int Wid { get; set; }
        /// <summary>
        /// 天气名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 天气内容
        /// </summary>
        public string Content { get; set; }
    }
}
