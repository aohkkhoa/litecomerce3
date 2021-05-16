using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Mặt hàng với các thông tin bổ sung
    /// </summary>
    public class ProductEx : Product
    {
        /// <summary>
        /// Danh sách các thuộc tính của mặt hàng
        /// </summary>
        public List<ProductAttribute> Attributes { get; set; }
        /// <summary>
        /// Danh sách các hình ảnh của mặt hàng
        /// </summary>
        public List<ProductGallery> Galleries { get; set; }
    }
}
