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

namespace AuthServiceLibrary
{
    public class LogLoginService
    {
        private string connectionString = string.Empty;
        private string tableName = "LogLogin";
        IMapper mapper;
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
    }
}
