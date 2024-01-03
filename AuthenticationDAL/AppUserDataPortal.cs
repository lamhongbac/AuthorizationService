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

        /// <summary>
        /// Đọc danh sách user theo CompanyAppID
        /// </summary>
        /// <param name="companyAppID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Đọc danh sách user làm manager theo phòng ban
        /// </summary>
        /// <param name="companyAppID"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task<List<AppUserUI>> ReadList(int companyAppID, string department)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE CompanyAppID = @CompanyAppID AND Department = @Department AND IsManager = @IsManager";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { CompanyAppID = companyAppID, Department = department, IsManager = true };
                    var dataUIs = await connection.QueryAsync<AppUserUI>(sql, param);
                    return dataUIs.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Đọc danh sách user theo phòng ban và ManagerID
        /// </summary>
        /// <param name="companyAppID"></param>
        /// <param name="department"></param>
        /// <param name="managerID"></param>
        /// <returns></returns>
        public async Task<List<AppUserUI>> ReadList(int companyAppID, string department, int managerID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE CompanyAppID = @CompanyAppID AND Department = @Department AND ManagerID = @ManagerID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { CompanyAppID = companyAppID, Department = department, ManagerID = managerID };
                    var dataUIs = await connection.QueryAsync<AppUserUI>(sql, param);
                    return dataUIs.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUserData> Read(int ID)
        {
            AppUserData data = new AppUserData();
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE ID = @ID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { ID = ID };
                    var dataUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(sql, param);
                    if(dataUI != null)
                    {
                        data.AppUser = dataUI;

                        string userStoreTableName = "UserStores";
                        string userStoreWhereString = " WHERE UserID = @UserID";
                        string userStoreSQL = "SELECT * FROM " + userStoreTableName + userStoreWhereString;
                        object userStoreParam = new { UserID = ID };
                        var userStoreUI = await connection.QueryAsync<UserStoreUI>(userStoreSQL, userStoreParam);
                        if(userStoreUI != null)
                        {
                            data.UserStores = userStoreUI.ToList();
                        }
                    }
                    

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUserData> Read(string UserName, int CompanyAppID)
        {
            AppUserData data = new AppUserData();
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string whereString = " WHERE UserName = @UserName AND CompanyAppID = @CompanyAppID";
                    string sql = "SELECT * FROM " + tableName + whereString;
                    object param = new { UserName = UserName, CompanyAppID = CompanyAppID };
                    var dataUI = await connection.QueryFirstOrDefaultAsync<AppUserUI>(sql, param);
                    if (dataUI != null)
                    {
                        data.AppUser = dataUI;

                        string userStoreTableName = "UserStores";
                        string userStoreWhereString = " WHERE UserID = @UserID";
                        string userStoreSQL = "SELECT * FROM " + userStoreTableName + userStoreWhereString;
                        object userStoreParam = new { UserID = dataUI.ID };
                        var userStoreUI = await connection.QueryAsync<UserStoreUI>(userStoreSQL, userStoreParam);
                        if (userStoreUI != null)
                        {
                            data.UserStores = userStoreUI.ToList();
                        }
                    }
                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Insert(AppUserData data)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await connection.InsertAsync(data.AppUser, trans);
                        if (result <= 0)
                        {
                            trans.Rollback();
                            return false;
                        }
                        else
                        {
                            if(data.UserStores != null && data.UserStores.Count > 0)
                            {
                                foreach (var item in data.UserStores)
                                {
                                    item.UserID = result;
                                }
                                var resultSub = await connection.InsertAsync(data.UserStores, trans);
                                if (resultSub <= 0)
                                {
                                    trans.Rollback();
                                    return false;
                                }
                            }
                            trans.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                
            }
            
        }

        public async Task<bool> Update(AppUserUI data, List<UserStoreUI> insertDatas, List<UserStoreUI> updateDatas, List<UserStoreUI> deleteDatas)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await connection.UpdateAsync(data, trans);
                        if (result == false)
                        {
                            return false;
                        }
                        else
                        {
                            var insert_result = true;
                            var updated_result = true;
                            var deleted_result = true;
                            if (updateDatas != null && updateDatas.Count > 0)
                            {
                                updated_result = await connection.UpdateAsync(updateDatas, trans);
                            }
                            if (insertDatas != null && insertDatas.Count > 0)
                            {
                                insert_result = await connection.InsertAsync(insertDatas, trans) > 0;
                            }
                            if (deleteDatas != null && deleteDatas.Count > 0)
                            {
                                deleted_result = await connection.DeleteAsync(deleteDatas, trans);
                            }
                            result = updated_result && insert_result && deleted_result;
                            if (result)
                            {
                                trans.Commit();
                                return true;

                            }
                            else
                            {
                                trans.Rollback();
                                return false;
                            }
                            
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                
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
                            parametter = new { ID = appUserUI.RoleID };
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
