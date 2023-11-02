﻿using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuthServices.Helpers;

namespace AuthServices
{
    public class AppUserService
    {
        private string connectionString = string.Empty;
        private string tableName = "AppUsers";
        IMapper mapper;
        public AppUserService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public AppUserService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BaseAppUser> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppUserUI> dataPortal = new GenericDataPortal<AppUserUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<AppUserUI> AppUserUIs = dataPortal.ReadList(whereString).Result;
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
            try
            {
                GenericDataPortal<AppUserUI> dataPortal = new GenericDataPortal<AppUserUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                AppUserUI AppUserUIs = dataPortal.Read(whereString, param).Result;
                if (AppUserUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                BaseAppUser BaseAppUser = mappingHelper.Map(AppUserUIs);

                //BaseAppUser BaseAppUser = mapper.Map<BaseAppUser>(AppUserUIs);
                result = true;
                errMessage = "Success";
                return BaseAppUser;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppUser GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppUserUI> dataPortal = new GenericDataPortal<AppUserUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                AppUserUI AppUserUIs = dataPortal.Read(whereString, param).Result;
                if (AppUserUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseAppUser, AppUserUI> mappingHelper = new IMappingHelper<BaseAppUser, AppUserUI>();
                BaseAppUser BaseAppUser = mappingHelper.Map(AppUserUIs);

                //BaseAppUser BaseAppUser = mapper.Map<BaseAppUser>(AppUserUIs);
                result = true;
                errMessage = "Success";
                return BaseAppUser;

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
            GenericDataPortal<AppUserUI> dataPortal = new GenericDataPortal<AppUserUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<AppUserUI, BaseAppUser> mappingHelper = new IMappingHelper<AppUserUI, BaseAppUser>();
                AppUserUI AppUserUI = mappingHelper.Map(data);

                //AppUserUI AppUserUI = mapper.Map<AppUserUI>(data);
                var result = await dataPortal.InsertAsync(AppUserUI, null);
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
            GenericDataPortal<AppUserUI> dataPortal = new GenericDataPortal<AppUserUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<AppUserUI, BaseAppUser> mappingHelper = new IMappingHelper<AppUserUI, BaseAppUser>();
                AppUserUI AppUserUI = mappingHelper.Map(data);

                //AppUserUI AppUserUI = mapper.Map<AppUserUI>(data);
                var result = await dataPortal.UpdateAsync(AppUserUI, null);
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
        public BODataProcessResult MarkDelete(BaseAppUser data)
        {
            return new BODataProcessResult();
        }
    }
}
