using AuthenticationDAL.DTO;
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
    public class UserRoleService
    {
        private string connectionString = string.Empty;
        private string tableName = "UserRoles";
        IMapper mapper;
        public UserRoleService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public UserRoleService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BaseUserRole> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserRoleUI> dataPortal = new GenericDataPortal<UserRoleUI>(connectionString, tableName);
                string whereString = string.Empty;

                List<UserRoleUI> UserRoleUIs = dataPortal.ReadList(whereString).Result;
                if (UserRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseUserRole, UserRoleUI> mappingHelper = new IMappingHelper<BaseUserRole, UserRoleUI>();
                List<BaseUserRole> BaseUserRoles = mappingHelper.Map(UserRoleUIs);

                //List<BaseUserRole> BaseUserRoles = mapper.Map<List<BaseUserRole>>(UserRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseUserRoles;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseUserRole GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserRoleUI> dataPortal = new GenericDataPortal<UserRoleUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                UserRoleUI UserRoleUIs = dataPortal.Read(whereString, param).Result;
                if (UserRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseUserRole, UserRoleUI> mappingHelper = new IMappingHelper<BaseUserRole, UserRoleUI>();
                BaseUserRole BaseUserRole = mappingHelper.Map(UserRoleUIs);

                //BaseUserRole BaseUserRole = mapper.Map<BaseUserRole>(UserRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseUserRole;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseUserRole GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserRoleUI> dataPortal = new GenericDataPortal<UserRoleUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                UserRoleUI UserRoleUIs = dataPortal.Read(whereString, param).Result;
                if (UserRoleUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseUserRole, UserRoleUI> mappingHelper = new IMappingHelper<BaseUserRole, UserRoleUI>();
                BaseUserRole BaseUserRole = mappingHelper.Map(UserRoleUIs);

                //BaseUserRole BaseUserRole = mapper.Map<BaseUserRole>(UserRoleUIs);
                result = true;
                errMessage = "Success";
                return BaseUserRole;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseUserRole data)
        {
            GenericDataPortal<UserRoleUI> dataPortal = new GenericDataPortal<UserRoleUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<UserRoleUI, BaseUserRole> mappingHelper = new IMappingHelper<UserRoleUI, BaseUserRole>();
                UserRoleUI UserRoleUI = mappingHelper.Map(data);

                //UserRoleUI UserRoleUI = mapper.Map<UserRoleUI>(data);
                var result = await dataPortal.InsertAsync(UserRoleUI, null);
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

        private UserRoleUI ConvertToData(BaseUserRole data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseUserRole data)
        {
            GenericDataPortal<UserRoleUI> dataPortal = new GenericDataPortal<UserRoleUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<UserRoleUI, BaseUserRole> mappingHelper = new IMappingHelper<UserRoleUI, BaseUserRole>();
                UserRoleUI UserRoleUI = mappingHelper.Map(data);

                //UserRoleUI UserRoleUI = mapper.Map<UserRoleUI>(data);
                var result = await dataPortal.UpdateAsync(UserRoleUI, null);
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
        public BODataProcessResult Delete(BaseUserRole data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseUserRole data)
        {
            return new BODataProcessResult();
        }
    }
}
