using AuthenticationDAL.DTO;
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
    public class MobileTrackingDataPortal
    {
        string _connectionString = "";
        string tableName = "mssMobileTracking";
        string tableNameDetail = "mssMobileTrackingDetail";

        public MobileTrackingDataPortal(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lây 1 danh sách để hiển thị trên UI
        /// </summary>
        /// <returns></returns>
        public async Task<List<MobileTrackingUI>> ReadListHeader()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM " + tableName;
                    object param = new object();
                    var dataUIs = await connection.QueryAsync<MobileTrackingUI>(sql, param);
                    return dataUIs.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy data header + n details
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public async Task<MobileTrackingData> ReadData(string userName)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
        SELECT 
    -- ===== TRACKING =====
tracking.ID,
    tracking.UserName,
    tracking.Token,
    tracking.IsIn,
    tracking.AppID,
    tracking.CreatedOn,
    tracking.CreatedBy,
    tracking.ModifiedOn,
    tracking.ModifiedBy,

    -- ===== DETAIL (PK PHẢI ĐỨNG ĐẦU) =====
    details.ID,
    details.UserName,
    details.DateIn,
    details.DateOut,
    details.Description,
    details.DeviceName,
    details.DeviceHid,
    details.Location,
    details.Platform,
    details.IP,
    details.TrackingType,
    details.Token,
    details.CreatedOn,
    details.CreatedBy,
    details.ModifiedOn,
    details.ModifiedBy
FROM mssMobileTracking tracking
LEFT JOIN mssMobileTrackingDetail details 
    ON tracking.UserName = details.UserName
WHERE tracking.UserName = @UserName;
";

            MobileTrackingData result = null;

            await connection.QueryAsync<MobileTrackingUI, MobileTrackingDetailUI, MobileTrackingData>(
                sql,
                (tracking, detail) =>
                {
                    if (result == null)
                    {
                        result = new MobileTrackingData
                        {
                            MobileTracking = tracking,
                            MobileTrackingDetails = new List<MobileTrackingDetailUI>()
                        };
                    }

                    if (detail != null)
                    {
                        result.MobileTrackingDetails.Add(detail);
                    }

                    return result;
                },
                new { UserName = userName },
                splitOn: "ID"
            );

            return result;
        }


        /// <summary>
        /// Insert 1 tracking data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Insert(MobileTrackingUI trackingUI, MobileTrackingDetailUI trackingDetailUI)
        {
            if (trackingUI == null || trackingDetailUI == null)
            {
                return false;
            }
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
                        var result = await connection.InsertAsync(trackingUI, trans);
                        if (result < 0)
                        {
                            trans.Rollback();
                            return false;
                        }
                        else
                        {
                            var resultSub = await connection.InsertAsync(trackingDetailUI, trans);
                            if (resultSub < 0)
                            {
                                trans.Rollback();
                                return false;
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

        /// <summary>
        /// Sử dụng cho cả 2 TH
        /// 1: Update parent + Insert vào bảng detail
        /// 2: Update parent + Update vào bảng detail
        /// </summary>
        /// <param name="trackingUI"></param>
        /// <param name="trackingDetailUI"></param>
        /// <returns></returns>
        public async Task<bool> Update(MobileTrackingUI trackingUI, MobileTrackingDetailUI trackingDetailUI, bool isInsert = false)
        {
            if (trackingUI == null || trackingDetailUI == null)
            {
                return false;
            }
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
                        result = await connection.UpdateAsync(trackingUI, trans);
                        if (result == false)
                        {
                            trans.Rollback();
                            return false;
                        }
                        else
                        {
                            if (isInsert)
                            {

                                result = await connection.InsertAsync(trackingDetailUI, trans) > -1;
                            }
                            else
                            {
                                result = await connection.UpdateAsync(trackingDetailUI, trans);

                            }
                            result = await connection.UpdateAsync(trackingDetailUI, trans);
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

        private async Task<MobileTrackingDetailUI> GetDetailUI(Guid detailID)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM " + tableNameDetail + " WHERE ID = @ID";
                    object param = new { ID = detailID };
                    var dataUI = await connection.QueryAsync<MobileTrackingDetailUI>(sql, param);
                    return (MobileTrackingDetailUI)dataUI;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
