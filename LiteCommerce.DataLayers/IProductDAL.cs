using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến hàng hóa
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ hàng hóa
        /// </summary>
        /// <returns></returns>
        List<Product> List();
        /// <summary>
        /// Lấy danh sách các mặt hàng(phân trang, tìm kiếm , lọc dữ liệu)
        /// </summary>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="categoryId">Mã loại hàng (0 nếu không loc theo loại hàng)</param>
        /// <param name="supplierId">Mã nhà cung cấp (0 nếu không loc theo loại hàng)</param>
        /// <param name="searchValue">Tên mặt hàng cần tìm kiếm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue);
        /// <summary>
        /// Đếm số lượng thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product Get(int productId);
        /// <summary>
        /// Lấy thông tin mặt hàng và các thông tin liên quan theo mã hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductEx GetEx(int productId);
        /// <summary>
        /// Bổ sung một mặt hàng mới (hàm trả về mã hàng được bổ sung nếu thành công, trả về 0 nếu không bổ sung được)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// Cập nhật thông tin mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// Xóa 1 mặt hàng (khi xóa 1 mặt hàng đồng thời xóa các thuộc tính và thư viện ảnh của mặt hàng)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool Delete(int productId);
        /// <summary>
        /// Lấy danh sách các thuộc tính của 1 product (sắp xếp theo DisplayOrder)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);
        /// <summary>
        /// Lấy thông tin chi tiết của 1 thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeId);
        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật thuộc tính
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// Xóa thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);
        /// <summary>
        /// Lấy danh sách hình ảnh của 1 mặt hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// Lấy thông tin của 1 ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);
        /// <summary>
        /// Bổ sung thông tin ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);
        /// <summary>
        /// Cập nhật thông tin ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        /// <summary>
        /// Xóa 1 ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);
    }
}
