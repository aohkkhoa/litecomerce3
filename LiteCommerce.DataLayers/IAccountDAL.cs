using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo các phép xử lý liên quan đến tk của user
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin đăng nhập user(Hàm trả về null nếu thông tin đăng nhập không hợp lệ)
        /// </summary>
        /// <param name="loginName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        Account Authorize(string loginName, string password);
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        bool ChangePassword(string accountId, string oldpassword, string newpassword);
        /// <summary>
        /// Lấy thông tin 1 tài khoản theo ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Account Get(string accountId);
    }
}
