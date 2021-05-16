using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class EmployeeAccountDAL : _BaseDAL, IAccountDAL
    {
        public EmployeeAccountDAL(string connectionString): base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string loginName, string password)
        {
            Account data = null;
            using (SqlConnection cn = GetConnection())
            {

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, Email, Photo
                                    FROM Employees
                                    WHERE Email=@loginName AND Password = @password
                                    ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Account()
                        {
                            UserName = dbReader["EmployeeID"].ToString(),
                            FullName=$"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = dbReader["Email"].ToString(),
                            Photo = dbReader["Photo"].ToString(),
                        };
                    }
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string accountId, string oldpassword, string newpassword)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET Password = @Password
                                    WHERE EmployeeID = @accountId;
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@accountId", accountId);
                cmd.Parameters.AddWithValue("@Password", newpassword);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account Get(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
