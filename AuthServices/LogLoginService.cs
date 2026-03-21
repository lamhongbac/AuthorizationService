using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AuthorizationService.DataTypes;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MSASharedLib.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuthServices.Models;
using System.Linq;

namespace AuthServiceLibrary
{
    public class LogLoginService
    {
        private string connectionString = string.Empty;
        private string tableName = "LogLogin";
        IMapper mapper;
        MobileTrackingDataPortal mobileTrackingDataPortal;
        public LogLoginService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public LogLoginService(IConfiguration configuration, IMapper mapper)
        {
            var configSection = configuration.GetSection("AppConfig");
            AppConfiguration appConfig = configSection.Get<AppConfiguration>();
            this.connectionString = configuration.GetConnectionString(appConfig.ProductMode);
            this.mapper = mapper;
        }

        public List<LogLoginUI> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<LogLoginUI> dataPortal = new GenericDataPortal<LogLoginUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<LogLoginUI> logLoginUIs = dataPortal.ReadList(whereString).Result;
                if (logLoginUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                result = true;
                errMessage = "Success";
                return logLoginUIs;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public LogLoginUI GetData(string ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<LogLoginUI> dataPortal = new GenericDataPortal<LogLoginUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                LogLoginUI logLoginUI = dataPortal.Read(whereString, param).Result;
                if (logLoginUI == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                result = true;
                errMessage = "Success";
                return logLoginUI;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }
        public async Task<BODataProcessResult> Create(LogLoginUI logLoginUI)
        {
            GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var result = await dataPortal.Insert(logLoginUI, null);
                if (result > -1)
                {
                    processResult.OK = true;
                    processResult.Message = "Success";
                }
                else
                {
                    processResult.Message = "Fail";
                    processResult.OK = false;
                }
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
            }
            return processResult;
        }

        public async Task<BODataProcessResult> Update(LogLoginUI logLoginUI)
        {
            GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var result = await dataPortal.UpdateAsync(logLoginUI, null);
                if (result == true)
                {
                    processResult.OK = true;
                    processResult.Message = "Success";
                }
                else
                {
                    processResult.Message = "Fail";
                    processResult.OK = false;
                }
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
            }
            return processResult;
        }

        public async Task<List<MobileTrackingUI>> GetListHeader()
        {
            mobileTrackingDataPortal = new MobileTrackingDataPortal(connectionString);
            List<MobileTrackingUI> trackingUIs = await mobileTrackingDataPortal.ReadListHeader();
            return trackingUIs;
        }

        public async Task<MobileTrackingData> GetDataByMemID(string memID)
        {
            mobileTrackingDataPortal = new MobileTrackingDataPortal(connectionString);
            MobileTrackingData trackingData = await mobileTrackingDataPortal.ReadData(memID);
            return trackingData;
        }

        /// <summary>
        /// Ghi log khi login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BODataProcessResult> LogLogin(LoginModel model, string userName)
        {
            BODataProcessResult result = new BODataProcessResult();
            mobileTrackingDataPortal = new MobileTrackingDataPortal(connectionString);

            MobileTrackingData mobileTrackingData = await mobileTrackingDataPortal.ReadData(userName);
            if (mobileTrackingData == null)
            {
                MobileTrackingUI trackingUI = new MobileTrackingUI();
                trackingUI.ID = Guid.NewGuid();
                trackingUI.UserName = userName;
                trackingUI.Token = model.Token;
                //trackingUI.AppID = model.AppID;
                trackingUI.IsIn = true;
                trackingUI.CreatedOn = DateTime.Now;
                trackingUI.CreatedBy = userName;
                trackingUI.ModifiedOn = DateTime.Now;
                trackingUI.ModifiedBy = userName;

                MobileTrackingDetailUI detailUI = new MobileTrackingDetailUI
                {
                    ID = Guid.NewGuid(),
                    //AppID = model.AppID,
                    DateIn = DateTime.Now,
                    IP = model.IP,
                    DeviceHid = model.Hid,
                    DeviceName = model.DeviceName,
                    Location = model.Location,
                    Platform = model.Platform,
                    Token = model.Token,
                    TrackingType = "Login",
                    UserName = userName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userName,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = userName
                };
                bool insertResut = await mobileTrackingDataPortal.Insert(trackingUI, detailUI);
                if (insertResut)
                {
                    result.OK = true;
                }
            }
            else
            {
                MobileTrackingUI trackingUI = mobileTrackingData.MobileTracking;
                trackingUI.Token = model.Token;
                //trackingUI.AppID = model.AppID;
                trackingUI.IsIn = true;
                trackingUI.CreatedOn = DateTime.Now;
                trackingUI.CreatedBy = userName;
                trackingUI.ModifiedOn = DateTime.Now;
                trackingUI.ModifiedBy = userName;

                MobileTrackingDetailUI detailUI = new MobileTrackingDetailUI
                {
                    ID = Guid.NewGuid(),
                    //AppID = model.AppID,
                    DateIn = DateTime.Now,
                    IP = model.IP,
                    DeviceHid = model.Hid,
                    DeviceName = model.DeviceName,
                    Location = model.Location,
                    Platform = model.Platform,
                    Token = model.Token,
                    TrackingType = "Login",
                    UserName = userName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userName,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = userName
                };
                bool insertResut = await mobileTrackingDataPortal.Update(trackingUI, detailUI, true);
                if (insertResut)
                {
                    result.OK = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Ghi log khi login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BODataProcessResult> LogLogout(string userName)
        {
            BODataProcessResult result = new BODataProcessResult();
            mobileTrackingDataPortal = new MobileTrackingDataPortal(connectionString);

            MobileTrackingData mobileTrackingData = await mobileTrackingDataPortal.ReadData(userName);
            if (mobileTrackingData == null)
            {
                result.OK = false;
                result.Message = "InValidData";
                return result;
            }
            else
            {
                MobileTrackingUI trackingUI = mobileTrackingData.MobileTracking;
                trackingUI.IsIn = false;
                trackingUI.CreatedOn = DateTime.Now;
                trackingUI.CreatedBy = userName;
                trackingUI.ModifiedOn = DateTime.Now;
                trackingUI.ModifiedBy = userName;

                MobileTrackingDetailUI detailUI = mobileTrackingData.MobileTrackingDetails.Where(x => x.DateOut == null).LastOrDefault();
                if (detailUI == null)
                {
                    result.OK = false;
                    result.Message = "InValidData";
                    return result;
                }
                else
                {
                    detailUI.DateOut = DateTime.Now;
                    detailUI.CreatedOn = DateTime.Now;
                    detailUI.CreatedBy = userName;
                    detailUI.ModifiedOn = DateTime.Now;
                    detailUI.ModifiedBy = userName;
                }
                bool insertResut = await mobileTrackingDataPortal.Update(trackingUI, detailUI);
                if (insertResut)
                {
                    result.OK = true;
                }
            }
            return result;
        }
    }
}
