using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 返回统一json格式.Models
{
    public class UserInfo
    {
        public int Age { get; set; }
        public bool Gender { get; set; }
        public bool? Gender2 { get; set; }
        public string Name { get; set; }
        public DateTime? RegTime { get; set; }
        public DateTime RegTime2 { get; set; }
    }
}
