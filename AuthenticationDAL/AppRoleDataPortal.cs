﻿using AuthenticationDAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace AuthenticationDAL
{
    public class AppRoleDataPortal
    {
        string _connectionString = "";
        string tableName = "AppRoles";
        public AppRoleDataPortal(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lây 1 danh sách AppRole để hiển thị trên UI
        /// </summary>
        /// <returns></returns>
        public async Task<List<AppRoleUI>> ReadList()
        {
            try
            {
                using(IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM " + tableName;
                    object param = new object();
                    var appRoleUIs = await connection.QueryAsync<AppRoleUI>(sql, param);
                    return appRoleUIs.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy 1 đối tượng AppRole theo Number
        /// Lấy danh sách RoleRight theo RoleID
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public async Task<AppRoleData> GetAppUserData(string Number, int CompanyAppID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    AppRoleData appRoleData = new AppRoleData();
                    string sql = "SELECT * FROM " + tableName + " WHERE Number = @Number AND CompanyAppID = @CompanyAppID";
                    object param = new { Number = Number, CompanyAppID = CompanyAppID };
                    var appRoleUI = await connection.QueryFirstOrDefaultAsync<AppRoleUI>(sql, param);
                    if(appRoleUI == null)
                    {
                        return null;
                    }
                    else
                    {
                        appRoleData.AppRoleUI = appRoleUI;
                        string sqlRoleRight = "SELECT * FROM RoleRights WHERE RoleID = @RoleID";
                        object paramRoleRight = new { RoleID = appRoleUI.ID };
                        var roleRights = await connection.QueryAsync<RoleRightUI>(sqlRoleRight, paramRoleRight);
                        if(roleRights == null)
                        {
                            return null;
                        }
                        else
                        {
                            appRoleData.RoleRightUIs = roleRights.ToList();
                        }
                    }
                    
                    return appRoleData;
                }
            }
            catch 
            {
                return null;
            }

        }

        /// <summary>
        /// Insert 1 AppRole
        /// Gán RoleID đã Insert cho danh RoleRight
        /// Nếu Insert AppRole thành công thì thực hiện Insert RoleRight
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Insert(AppRoleData data)
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
                        var result = await connection.InsertAsync(data.AppRoleUI, trans);
                        if (result <= 0)
                        {
                            trans.Dispose();
                            return false;
                        }
                        else
                        {
                            foreach (var item in data.RoleRightUIs)
                            {
                                item.RoleID = result;
                            }
                            var resultSub = await connection.InsertAsync(data.RoleRightUIs, trans);
                            if (resultSub <= 0)
                            {
                                trans.Dispose();
                                return false;
                            }
                            trans.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        trans.Dispose();
                        return false;
                    }
                }
                
            }
            
        }

        public async Task<bool> Update(AppRoleUI updateAppRoleUI, List<RoleRightUI> insertRights,
            List<RoleRightUI> updateRights, List<RoleRightUI> deleteRights)
        {
            bool result = false;
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
                         result = await connection.UpdateAsync(updateAppRoleUI, trans);
                        if (result == false)
                        {
                           
                            return false;
                        }
                        else
                        {
                            var insert_result = true;
                            var updated_result = true;
                            var deleted_result = true;
                            if (updateRights != null && updateRights.Count > 0)
                            {
                                updated_result = await connection.UpdateAsync(updateRights, trans);
                            }
                            if (insertRights != null && insertRights.Count > 0)
                            {
                                insert_result = await connection.InsertAsync(insertRights, trans)>0;
                            }
                            if(deleteRights != null && deleteRights.Count > 0)
                            {
                                deleted_result = await connection.DeleteAsync(deleteRights, trans);
                            }
                            result = updated_result && insert_result && deleted_result;
                            if (result)
                            {
                                trans.Commit();
                               
                            }
                            else
                            {
                                trans.Rollback();    
                            }
                            
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        result = false;
                    }
                }

            }
            return result;
        }

        public async Task<bool> Delete(AppRoleData data)
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
                        var result = await connection.DeleteAsync(data.AppRoleUI, trans);
                        if (result == false)
                        {
                            trans.Dispose();
                            return false;
                        }
                        else
                        {
                            var resultSub = await connection.DeleteAsync(data.RoleRightUIs, trans);
                            if (resultSub == false)
                            {
                                trans.Dispose();
                                return false;
                            }
                            trans.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        trans.Dispose();
                        return false;
                    }
                }

            }

        }
        public async Task<bool> MarkDelete(AppRoleData data)
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
                        var result = await connection.UpdateAsync(data.AppRoleUI, trans);
                        if (result == false)
                        {
                            trans.Dispose();
                            return false;
                        }
                        else
                        {
                            var resultSub = await connection.UpdateAsync(data.RoleRightUIs, trans);
                            if (resultSub == false)
                            {
                                trans.Dispose();
                                return false;
                            }
                            trans.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        trans.Dispose();
                        return false;
                    }
                }

            }

        }
    }
}
