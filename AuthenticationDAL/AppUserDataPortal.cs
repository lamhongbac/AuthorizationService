using AuthenticationDAL.DTO;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AuthenticationDAL
{
    public class AppUserDataPortal
    {
        string _connectionString = "";
        string tableName = "AppUsers";
        public AppUserDataPortal(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy thông tin đầy đủ của 1 user. Bao gồm AppUserUI, AppRoleUI, List RoleRight
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUserData> GetAppUserData(string userName, int appID)
        {
            try
            {
                AppUserData data = new AppUserData();
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string Sql = "SELECT * FROM " + tableName + " WHERE UserName=@UserName AND CompanyAppID=@CompanyAppID";
                    object parametter = new { UserName = userName, CompanyAppID = appID };
                    AppUserUI appUserUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(Sql, parametter);
                    if (appUserUI == null)
                    {
                        return null;
                    }
                    else
                    {
                        //Get AppRole
                        Sql = "SELECT * FROM AppRoles WHERE RoleID=@RoleID";
                        parametter = new { RoleID = appUserUI.RoleID };
                        AppRoleUI appRoleUI = await connection.QueryFirstOrDefaultAsync<AppRoleUI>(Sql, parametter);

                        //Get RoleRights
                        Sql = "SELECT * FROM RoleRights WHERE RoleID=@RoleID";
                        parametter = new { RoleID = appUserUI.RoleID };
                        List<RoleRightUI> roleRightUIs = (List<RoleRightUI>)await connection.QueryAsync<List<RoleRightUI>>(Sql, parametter);

                        data.AppUser = appUserUI;
                        data.AppRole = appRoleUI;
                        data.RoleRights = roleRightUIs;
                    }
                    return data;
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
    }
}
