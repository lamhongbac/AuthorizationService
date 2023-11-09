using AuthenticationDAL.DTO;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public async Task<List<AppUserUI>> ReadList(int companyAppID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE CompanyAppID = @CompanyAppID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { CompanyAppID = companyAppID };
                    var dataUIs = await connection.QueryAsync<AppUserUI>(sql, param);
                    return dataUIs.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUserUI> Read(int ID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE ID = @ID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { ID = ID };
                    var dataUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(sql, param);
                    return dataUI;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUserUI> Read(string UserName, int CompanyAppID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE UserName = @UserName AND CompanyAppID = @CompanyAppID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { UserName = UserName, CompanyAppID = CompanyAppID };
                    var dataUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(sql, param);
                    return dataUI;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Insert(AppUserUI data)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                { 
                    var result = await connection.InsertAsync(data);
                    if(result <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> Update(AppUserUI data)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var result = await connection.UpdateAsync(data);
                    if (result == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> MarkDelete(AppUserUI data)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var result = await connection.UpdateAsync(data);
                    if (result == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin đầy đủ của 1 user. Bao gồm AppUserUI, AppRoleUI, List RoleRight
        /// b1 lay CompanyAppID (company,App)ID
        /// b2 lay ra user (userName, CompanyAppID)
        /// b3 lay role (theo roleID)
        /// b4 lay ra danh sach Right theo (AppID, RoleID)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUserData> GetAppUserData(string userName, int companyID, int appID)
        {
            try
            {
                AppUserData data = new AppUserData();
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    //Get CompanyAppID
                    string Sql = "SELECT * FROM CompanyApplication WHERE CompanyID=@CompanyID AND AppID = @AppID";
                    object parametter = new { CompanyID = companyID, appID = appID };
                    CompanyApplicationUI companyApplicationUI = await connection.QueryFirstOrDefaultAsync<CompanyApplicationUI>(Sql, parametter);
                    if (companyApplicationUI == null)
                    {
                        return null;
                    }
                    else
                    {
                        Sql = "SELECT * FROM " + tableName + " WHERE UserName=@UserName AND CompanyAppID=@CompanyAppID";
                        parametter = new { UserName = userName, CompanyAppID = companyApplicationUI.ID };
                        AppUserUI appUserUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(Sql, parametter);
                        if (appUserUI == null)
                        {
                            return null;
                        }
                        else
                        {
                            data.AppUser = appUserUI;
                            //Get AppRoleUI
                            Sql = "SELECT * FROM AppRoles WHERE ID=@ID";
                            parametter = new { ID = appID };
                            AppRoleUI appRoleUI = await connection.QueryFirstOrDefaultAsync<AppRoleUI>(Sql, parametter);
                            data.AppRole = appRoleUI;

                            //Get CompanyUI
                            Sql = "SELECT * FROM Companies WHERE ID=@ID";
                            parametter = new { ID = companyID };
                            CompanyUI companyUI = await connection.QueryFirstOrDefaultAsync<CompanyUI>(Sql, parametter);
                            data.Company = companyUI;

                            //Get List RoleRightUI
                            Sql = "SELECT * FROM RoleRights WHERE RoleID=@RoleID";
                            parametter = new { RoleID = appUserUI.RoleID };
                            var roleRightUIs = await connection.QueryAsync<RoleRightUI>(Sql, parametter);
                            data.RoleRights = (List<RoleRightUI>)roleRightUIs;
                        }
                        return data;
                    }
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
