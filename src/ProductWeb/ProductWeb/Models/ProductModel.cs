using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWeb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
