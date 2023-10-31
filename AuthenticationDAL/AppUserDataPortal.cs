using AuthenticationDAL.DTO;
using Dapper;
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
        /// Lấy thông tin đầy đủ của 1 user. Bao gồm AppUserUI, AppRoleUI, App
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUserData> GetAppUserData(string userName)
        {
            try
            {
                AppUserData data = new AppUserData();
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string Sql = "SELECT * FROM " + tableName + " WHERE UserName=@UserName";
                    object parametter = new { UserName = userName };
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
                        Sql = "SELECT * FROM AppRoles WHERE RoleID=@RoleID";
                        parametter = new { RoleID = appUserUI.RoleID };
                        AppRoleUI appRoleUI = await connection.QueryFirstOrDefaultAsync<AppRoleUI>(Sql, parametter);

                        data.AppUser = appUserUI;
                        data.AppRole = appRoleUI;
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
