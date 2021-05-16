using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{

    /// <summary>
    /// 
    /// </summary>
    public static class AccountService
    {
        private static IAccountDAL AccountDB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <param name="accountType"></param>
        public static void Init(DatabaseTypes dbType, string connectionString, AccountType accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if (accountType == AccountType.Employee)
                        AccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
                    else
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);

                    break;
                default:
                    throw new Exception("DataBase Type is not Supported");
            }
        }
        public static Account Authorize(string loginName, string password)
        {
            return AccountDB.Authorize(loginName, password);
        }
        public static bool ChangePassword(string accountId, string oldpassword, string newpassword)
        {
            return AccountDB.ChangePassword(accountId, oldpassword, newpassword);
        }
        public static Account Get(string accountId)
        {
            return AccountDB.Get(accountId);
        }
    }
}
