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
        private string tableName = "AppRoles";
        IMapper mapper;
        public AppRoleService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<BaseAppRole> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppRoleUI> dataPortal = new GenericDataPortal<AppRoleUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<AppRoleUI> AppRoleUIs = dataPortal.ReadList(whereString).Result;
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

        public BaseAppRole GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppRoleUI> dataPortal = new GenericDataPortal<AppRoleUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                AppRoleUI AppRoleUIs = dataPortal.Read(whereString, param).Result;
                if (AppRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseAppRole BaseAppRole = mapper.Map<BaseAppRole>(AppRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseAppRole;

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
                GenericDataPortal<AppRoleUI> dataPortal = new GenericDataPortal<AppRoleUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                AppRoleUI AppRoleUIs = dataPortal.Read(whereString, param).Result;
                if (AppRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseAppRole BaseAppRole = mapper.Map<BaseAppRole>(AppRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseAppRole;

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
            GenericDataPortal<AppRoleUI> dataPortal = new GenericDataPortal<AppRoleUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                var result = await dataPortal.InsertAsync(AppRoleUI, null);
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
            GenericDataPortal<AppRoleUI> dataPortal = new GenericDataPortal<AppRoleUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppRoleUI AppRoleUI = mapper.Map<AppRoleUI>(data);
                var result = await dataPortal.UpdateAsync(AppRoleUI, null);
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
        public BODataProcessResult Delete(BaseAppRole data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseAppRole data)
        {
            return new BODataProcessResult();
        }
    }
}
