using System;

namespace SwaggerAndJwt
{
    public class WeatherForecast
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// 华氏温度
        /// </summary>

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 备注
        /// </summary>

        public string Summary { get; set; }
    }
}
