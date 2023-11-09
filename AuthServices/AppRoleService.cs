using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuthServices.Helpers;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

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

        public AppRoleService(string connectionString)
        {
            this.connectionString = connectionString;
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
                //List<BaseAppRole> BaseAppRoles = new List<BaseAppRole>();

                //foreach (var item in  AppRoleUIs)
                //{
                //    IMappingHelper<BaseAppRole, AppRoleUI> mappingHelper = new IMappingHelper<BaseAppRole, AppRoleUI>();
                //    BaseAppRole baseData = mappingHelper.Map(item);
                //    BaseAppRoles.Add(baseData);
                //}
                IMappingHelper<BaseAppRole, AppRoleUI> mappingHelper = new IMappingHelper<BaseAppRole, AppRoleUI>();
                List<BaseAppRole> BaseAppRoles = mappingHelper.Map(AppRoleUIs);

                //List<BaseAppRole> BaseAppRoles = mapper.Map<List<BaseAppRole>>(AppRoleUIs);
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

        public BaseAppRole GetData(string Number, int CompanyAppID, out string errMessage, out bool result)
        {
            try
            {
                AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
                AppRoleData AppRoleData = dataPortal.GetAppUserData(Number, CompanyAppID).Result;
                if (AppRoleData == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppRole, AppRoleUI> mappingHelper = new IMappingHelper<BaseAppRole, AppRoleUI>();
                BaseAppRole baseData = mappingHelper.Map(AppRoleData.AppRoleUI);

                IMappingHelper<BaseRoleRight, RoleRightUI> mappingRightHelper = new IMappingHelper<BaseRoleRight, RoleRightUI>();
                baseData.Rights = mappingRightHelper.Map(AppRoleData.RoleRightUIs);

                //BaseAppRole baseData = mapper.Map<BaseAppRole>(AppRoleData.AppRoleUI);
                //baseData.Rights = mapper.Map<List<BaseRoleRight>>(AppRoleData.RoleRightUIs);
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
                AppRoleData exists = await dataPortal.GetAppUserData(data.Number, data.CompanyAppID);
                if (exists != null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data is exist";
                    return processResult;
                }


                IMappingHelper<AppRoleUI, BaseAppRole> mappingHelper = new IMappingHelper<AppRoleUI, BaseAppRole>();
                AppRoleUI AppRoleUI = mappingHelper.Map(data);

                IMappingHelper<RoleRightUI, BaseRoleRight> mappingRightHelper = new IMappingHelper<RoleRightUI, BaseRoleRight>();
                List<RoleRightUI> roleRightUIs = mappingRightHelper.Map(data.Rights);

                //AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                //List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

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
        /// <summary>
        /// Update Role
        /// Co the phat sinh role moi
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BODataProcessResult> Update(BaseAppRole data)
        {
            AppRoleDataPortal dataPortal = new AppRoleDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppRoleData existsAppRoleData =await dataPortal.GetAppUserData(data.Number, data.CompanyAppID);
                if (existsAppRoleData == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }
                //b1 xac dinh data update bang AppRole
                IMappingHelper<AppRoleUI, BaseAppRole> mappingHelper = new IMappingHelper<AppRoleUI, BaseAppRole>();                
                //Exist AppRoleUI
                AppRoleUI updateAppRoleUI = mappingHelper.Map(data);
                //b2 xac dinh data to insert or Update bang RoleRight
               

                IMappingHelper<RoleRightUI, BaseRoleRight> mappingRightHelper = new IMappingHelper<RoleRightUI, BaseRoleRight>();

                //Exist Rights= cac right dang co trong CSDL
                List<RoleRightUI> existRights = existsAppRoleData.RoleRightUIs;

                List<RoleRightUI> tobeUpdateRights = mappingRightHelper.Map(data.Rights);
                List<RoleRightUI> insertRights = new List<RoleRightUI>();
                List<RoleRightUI> updateRights = new List<RoleRightUI>();
                List<RoleRightUI> deleteRights = new List<RoleRightUI>();
                // lay 1 dong trong exist do voi data tu para, neu no kg ton tai la insert,
                //nguoc lai la Update
                foreach (var item in tobeUpdateRights)
                {
                    RoleRightUI newRight = existRights.FirstOrDefault(x => x.AppObjectID == item.AppObjectID);
                    //neu ton tai trong danh sach can update la old items=> can update
                    if (newRight != null && newRight.Id != 0)
                    {
                        updateRights.Add(item); 
                    }
                    else
                    {
                        insertRights.Add(item);
                    }
                }

                foreach(var item in existRights)
                {
                    RoleRightUI newRight = tobeUpdateRights.FirstOrDefault(x => x.AppObjectID == item.AppObjectID);
                    if(newRight == null)
                    {
                        deleteRights.Add(item);
                    }
                }

              

                //AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                //List<RoleRightUI> roleRightUIs = mapper.Map<List<RoleRightUI>>(data.Rights);

                
                var result = await dataPortal.Update(updateAppRoleUI, insertRights,updateRights, deleteRights);
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
                var exists = dataPortal.GetAppUserData(data.Number, data.CompanyAppID);
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
                var exists = dataPortal.GetAppUserData(data.Number, data.CompanyAppID);
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
