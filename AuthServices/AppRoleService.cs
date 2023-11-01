using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthServices
{
    public class AppRoleService
    {
        private string connectionString = string.Empty;
        IMapper mapper;
        public AppRoleService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<BaseAppRole> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
                List<AppRoleUI> AppRoleUIs = dataPortal.ReadList().Result;
                if (AppRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                List<BaseAppRole> BaseAppRoles = mapper.Map<List<BaseAppRole>>(AppRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseAppRoles;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppRole GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
                AppRoleData AppRoleData = dataPortal.GetAppUserData(Number).Result;
                if (AppRoleData == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseAppRole baseData = mapper.Map<BaseAppRole>(AppRoleData.AppRoleUI);
                baseData.Rights = mapper.Map<List<BaseRoleRight>>(AppRoleData.RoleRightUIs);
                result = true;
                errMessage = "Success";
                return baseData;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseAppRole data)
        {
            AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var exists = dataPortal.GetAppUserData(data.Number);
                if (exists != null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data is exist";
                    return processResult;
                }
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

                AppRoleData appRoleData = new AppRoleData();
                appRoleData.AppRoleUI = AppRoleUI;
                appRoleData.RoleRightUIs = roleRightUIs;

                var result = await dataPortal.Insert(appRoleData);
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

        private AppRoleUI ConvertToData(BaseAppRole data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseAppRole data)
        {
            AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var exists = dataPortal.GetAppUserData(data.Number);
                if (exists == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

                AppRoleData appRoleData = new AppRoleData();
                appRoleData.AppRoleUI = AppRoleUI;
                appRoleData.RoleRightUIs = roleRightUIs;

                var result = await dataPortal.Update(appRoleData);
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
        public async Task<BODataProcessResult> Delete(BaseAppRole data)
        {
            AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var exists = dataPortal.GetAppUserData(data.Number);
                if (exists == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

                AppRoleData appRoleData = new AppRoleData();
                appRoleData.AppRoleUI = AppRoleUI;
                appRoleData.RoleRightUIs = roleRightUIs;

                var result = await dataPortal.Delete(appRoleData);
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
        public async Task<BODataProcessResult> MarkDelete(BaseAppRole data)
        {
            AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                var exists = dataPortal.GetAppUserData(data.Number);
                if (exists == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

                AppRoleData appRoleData = new AppRoleData();
                appRoleData.AppRoleUI = AppRoleUI;
                appRoleData.RoleRightUIs = roleRightUIs;

                var result = await dataPortal.MarkDelete(appRoleData);
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
