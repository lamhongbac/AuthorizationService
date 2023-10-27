using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDAL
{
    public class GenericDataPortal<T>
    {
        string tableName = string.Empty;

        private string _connectionString;
        public GenericDataPortal(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            this.tableName = tableName;
        }

        /// <summary>
        /// Tra ve 1 doi tuong su dung dapper
        /// </summary>
        /// <param name="whereString"></param>
        /// <param name="parametter"></param>
        /// <returns></returns>
        public async Task<T> Read(string whereString, object parametter)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    //1. Build SQL
                    string Sql = "SELECT * FROM " + tableName + " WHERE " + whereString;
                    //2. Ket qua tra ve
                    T data = await connection.QueryFirstOrDefaultAsync<T>(Sql, parametter);

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

        public async Task<List<T>> ReadList(string whereString, object parametters = null, string orderCommand = "")
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string Sql = "SELECT * FROM " + tableName;
                    if (!string.IsNullOrEmpty(whereString))
                    {
                        string orderByString;
                        if (string.IsNullOrEmpty(orderCommand))
                        {
                            orderByString = "";
                        }
                        else
                        {
                            orderByString = " ORDER BY " + orderCommand;
                        }
                        Sql += " WHERE " + whereString + orderByString;

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        if (parametters != null)
                        {
                            var data = await connection.QueryAsync<T>(Sql, parametters);
                            return data.ToList();
                        }
                        else
                        {
                            var data = await connection.QueryAsync<T>(Sql);
                            return data.ToList();
                        }
                    }
                    else
                    {
                        var data = await connection.QueryAsync<T>(Sql);
                        return data.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                //log message
                throw;
            }
        }

        public async Task<bool> UpdateAsync<T>(T entityToUpdate, IDbTransaction transaction) where T : class
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    bool result = false;
                    result = await connection.UpdateAsync(entityToUpdate, transaction);
                    return result;
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
        //connection.InsertAsync<BrandUI>(model);
        public async Task<bool> InsertAsync<T>(T entityToInsert, IDbTransaction transaction) where T : class
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    int rec = -1;
                    rec = await connection.InsertAsync(entityToInsert, transaction);
                    return rec > 0;
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
        public async Task<int> Insert<T>(T entityToInsert, IDbTransaction transaction) where T : class
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {

                    int rec = -1;
                    rec = await connection.InsertAsync(entityToInsert, transaction);
                    return rec;
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
