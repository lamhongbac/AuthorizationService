using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AutoMapper.Execution;
using SharedLib;
using AuthServices.Models;
using System.Linq;
using MSASharedLib.Utils;
using SharedLib.Authentication;
using AuthorizationService.DataTypes;
using Microsoft.Extensions.Configuration;
using MSASharedLib.DataTypes;

namespace AuthServices
{
    public class AppUserService
    {
        private string connectionString = string.Empty;
        private string tableName = "AppUsers";
        IMapper mapper;
   
        public AppUserService(IConfiguration configuration, IMapper mapper)
        {
            var configSection = configuration.GetSection("AppConfig");
            AppConfiguration appConfig = configSection.Get<AppConfiguration>();
            this.connectionString = configuration.GetConnectionString(appConfig.ProductMode);
            this.mapper = mapper;
        }
        public List<BaseAppUser> GetDatas(int companyAppID, out string errMessage, out bool result)
        {
            try
            {
                AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
                string whereString = string.Empty;
                List<AppUserUI> AppUserUIs = dataPortal.ReadList(companyAppID).Result;
                if (AppUserUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                List<BaseAppUser> BaseAppUsers = mappingHelper.Map(AppUserUIs);

                //List<BaseAppUser> BaseAppUsers = mapper.Map<List<BaseAppUser>>(AppUserUIs);
                result = true;
                errMessage = "Success";
                return BaseAppUsers;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public List<BaseAppUser> GetDatas(int companyAppID, string department, out string errMessage, out bool result)
        {
            try
            {
                AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
                string whereString = string.Empty;
                List<AppUserUI> AppUserUIs = dataPortal.ReadList(companyAppID, department).Result;
                if (AppUserUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                List<BaseAppUser> BaseAppUsers = mappingHelper.Map(AppUserUIs);

                //List<BaseAppUser> BaseAppUsers = mapper.Map<List<BaseAppUser>>(AppUserUIs);
                result = true;
                errMessage = "Success";
                return BaseAppUsers;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public List<BaseAppUser> GetDatas(int companyAppID, string department, int managerID,  out string errMessage, out bool result)
        {
            try
            {
                AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
                string whereString = string.Empty;
                List<AppUserUI> AppUserUIs = dataPortal.ReadList(companyAppID, department, managerID).Result;
                if (AppUserUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                List<BaseAppUser> BaseAppUsers = mappingHelper.Map(AppUserUIs);

                //List<BaseAppUser> BaseAppUsers = mapper.Map<List<BaseAppUser>>(AppUserUIs);
                result = true;
                errMessage = "Success";
                return BaseAppUsers;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppUser GetData(int ID, out string errMessage, out bool result)
        {
            AppUserData AppUserData = new AppUserData();
            try
            {
                AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);

                AppUserData = dataPortal.Read(ID).Result;
                if (AppUserData.AppUser == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                BaseAppUser BaseAppUser = mappingHelper.Map(AppUserData.AppUser);

                IMappingHelper<BaseUserStore, UserStoreUI> mappingUserStoreHelper = new IMappingHelper<BaseUserStore, UserStoreUI>();
                if (BaseAppUser != null)
                {
                    BaseAppUser.BaseUserStores = mappingUserStoreHelper.Map(AppUserData.UserStores);
                    result = true;
                    errMessage = "Success";
                    return BaseAppUser;
                }
                result = false;
                errMessage = "false";
                return null;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppUser GetData(string UserName, int CompanyAppID, out string errMessage, out bool result)
        {
            AppUserData AppUserData = new AppUserData();
            try
            {
                AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
                AppUserData = dataPortal.Read(UserName, CompanyAppID).Result;
                if (AppUserData.AppUser == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                BaseAppUser BaseAppUser = mappingHelper.Map(AppUserData.AppUser);

                IMappingHelper<BaseUserStore, UserStoreUI> mappingUserStoreHelper = new IMappingHelper<BaseUserStore, UserStoreUI>();
                if(BaseAppUser != null)
                {
                    BaseAppUser.BaseUserStores = mappingUserStoreHelper.Map(AppUserData.UserStores);
                    result = true;
                    errMessage = "Success";
                    return BaseAppUser;
                }
                result = false;
                errMessage = "false";
                return null;
                //BaseAppUser BaseAppUser = mapper.Map<BaseAppUser>(AppUserUIs);
                

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseAppUser data)
        {
            AppUserData AppUserData = new AppUserData();
            AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppUserData = await dataPortal.Read(data.UserName, data.CompanyAppID);
                if (AppUserData.AppUser != null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data is exist";
                    return processResult;
                }

                IMappingHelper<AppUserUI, BaseAppUser> mappingHelper = new IMappingHelper<AppUserUI, BaseAppUser>();
                AppUserData.AppUser = mappingHelper.Map(data);

                //Kiểm tra role selected là RM hay không
                GenericDataPortal<AppRoleUI> genericDataPortal = new GenericDataPortal<AppRoleUI>(connectionString, "AppRoles");
                string whereString = "ID = @ID";
                object param = new { ID = data.RoleID };
                AppRoleUI existRole = await genericDataPortal.Read(whereString, param);
                if (existRole == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Role note found";
                    return processResult;
                }

                if(existRole.IsStoreAdmin == false)
                {
                    data.BaseUserStores = new List<BaseUserStore>();
                }

                IMappingHelper<UserStoreUI, BaseUserStore> mappingUserStoreHelper = new IMappingHelper<UserStoreUI, BaseUserStore>();
                AppUserData.UserStores = mappingUserStoreHelper.Map(data.BaseUserStores);

                //AppUserUI AppUserUI = mapper.Map<AppUserUI>(data);
                var result = await dataPortal.Insert(AppUserData);
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

        private AppUserUI ConvertToData(BaseAppUser data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseAppUser data)
        {
            AppUserData AppUserData = new AppUserData();
            AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppUserData = await dataPortal.Read(data.UserName, data.CompanyAppID);
                if (AppUserData.AppUser == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }

                IMappingHelper<AppUserUI, BaseAppUser> mappingHelper = new IMappingHelper<AppUserUI, BaseAppUser>();
                AppUserUI updateUserUI = mappingHelper.Map(data);

                //Kiểm tra role selected là RM hay không
                GenericDataPortal<AppRoleUI> genericDataPortal = new GenericDataPortal<AppRoleUI>(connectionString, "AppRoles");
                string whereString = "ID = @ID";
                object param = new { ID = data.RoleID };
                AppRoleUI existRole = await genericDataPortal.Read(whereString, param);
                if (existRole == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Role note found";
                    return processResult;
                }

                if (existRole.IsStoreAdmin == false)
                {
                    data.BaseUserStores = new List<BaseUserStore>();
                }

                IMappingHelper<UserStoreUI, BaseUserStore> mappingUserStoreHelper = new IMappingHelper<UserStoreUI, BaseUserStore>();
                List<UserStoreUI> updateUserStores = mappingUserStoreHelper.Map(data.BaseUserStores);

                List<UserStoreUI> existUserStores = AppUserData.UserStores;
                List<UserStoreUI> insertDatas = new List<UserStoreUI>();
                List<UserStoreUI> updateDatas = new List<UserStoreUI>();
                List<UserStoreUI> deleteDatas = new List<UserStoreUI>();

                if(updateUserStores != null)
                {
                    foreach(var item in updateUserStores)
                    {
                        if(existUserStores != null && existUserStores.Count > 0)
                        {
                            UserStoreUI userStoreUI = existUserStores.FirstOrDefault(x => x.UserID == item.UserID && x.StoreID == item.StoreID);
                            if(userStoreUI != null && userStoreUI.ID != 0)
                            {
                                updateDatas.Add(item);
                            }
                            else
                            {
                                insertDatas.Add(item);
                            }
                        }
                        else
                        {
                            insertDatas.Add(item);
                        }
                    }

                    foreach(var item in existUserStores)
                    {
                        UserStoreUI userStoreUI = updateUserStores.FirstOrDefault(x => x.UserID == item.UserID && x.StoreID == item.StoreID);
                        if(userStoreUI == null)
                        {
                            deleteDatas.Add(item);
                        }
                    }
                }

                //AppUserUI AppUserUI = mapper.Map<AppUserUI>(data);
                var result = await dataPortal.Update(updateUserUI, insertDatas, updateDatas, deleteDatas);
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
        public BODataProcessResult Delete(BaseAppUser data)
        {
            return new BODataProcessResult();
        }
        public async Task<BODataProcessResult> MarkDelete(BaseAppUser data)
        {
            AppUserData AppUserData = new AppUserData();
            AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppUserData = await dataPortal.Read(data.UserName, data.CompanyAppID);
                if (AppUserData.AppUser == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }

                AppUserData.AppUser.IsDeleted = true;
                AppUserData.AppUser.ModifiedOn = data.ModifiedOn;
                AppUserData.AppUser.ModifiedBy = data.ModifiedBy;
                //IMappingHelper<AppUserUI, BaseAppUser> mappingHelper = new IMappingHelper<AppUserUI, BaseAppUser>();
                //AppUserUI AppUserUI = mappingHelper.Map(data);

                //AppUserUI AppUserUI = mapper.Map<AppUserUI>(data);
                var result = await dataPortal.MarkDelete(AppUserData.AppUser);
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

        public async Task<BODataProcessResult> AdminChangePass(ChangePwdModel model)
        {
            AppUserData AppUserData = new AppUserData();
            AppUserDataPortal dataPortal = new AppUserDataPortal(connectionString);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppUserData = await dataPortal.Read(model.UserName, model.CompanyAppID);
                if (AppUserData.AppUser == null)
                {
                    processResult.OK = false;
                    processResult.Message = "Data not found";
                    return processResult;
                }
                AppUserData.AppUser.Pwd = model.NewPassword;
                AppUserData.AppUser.PwdKey = model.PwdKey;
                AppUserData.AppUser.ModifiedBy = model.ModifiedBy;
                AppUserData.AppUser.ModifiedOn = DateTime.Now;
                var result = await dataPortal.Update(AppUserData.AppUser, null, null, null);
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
