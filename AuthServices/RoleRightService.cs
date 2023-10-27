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
    public class RoleRightService
    {
        private string connectionString = string.Empty;
        private string tableName = "RoleRights";
        IMapper mapper;
        public RoleRightService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<BaseRoleRight> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<RoleRightUI> dataPortal = new GenericDataPortal<RoleRightUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<RoleRightUI> RoleRightUIs = dataPortal.ReadList(whereString).Result;
                if (RoleRightUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                List<BaseRoleRight> BaseRoleRights = mapper.Map<List<BaseRoleRight>>(RoleRightUIs);
                result = true;
                errMessage = "Success";
                return BaseRoleRights;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseRoleRight GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<RoleRightUI> dataPortal = new GenericDataPortal<RoleRightUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                RoleRightUI RoleRightUIs = dataPortal.Read(whereString, param).Result;
                if (RoleRightUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseRoleRight BaseRoleRight = mapper.Map<BaseRoleRight>(RoleRightUIs);
                result = true;
                errMessage = "Success";
                return BaseRoleRight;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseRoleRight GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<RoleRightUI> dataPortal = new GenericDataPortal<RoleRightUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                RoleRightUI RoleRightUIs = dataPortal.Read(whereString, param).Result;
                if (RoleRightUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseRoleRight BaseRoleRight = mapper.Map<BaseRoleRight>(RoleRightUIs);
                result = true;
                errMessage = "Success";
                return BaseRoleRight;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseRoleRight data)
        {
            GenericDataPortal<RoleRightUI> dataPortal = new GenericDataPortal<RoleRightUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                RoleRightUI RoleRightUI = mapper.Map<RoleRightUI>(data);
                var result = await dataPortal.InsertAsync(RoleRightUI, null);
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

        private RoleRightUI ConvertToData(BaseRoleRight data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseRoleRight data)
        {
            GenericDataPortal<RoleRightUI> dataPortal = new GenericDataPortal<RoleRightUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                RoleRightUI RoleRightUI = mapper.Map<RoleRightUI>(data);
                var result = await dataPortal.UpdateAsync(RoleRightUI, null);
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
        public BODataProcessResult Delete(BaseRoleRight data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseRoleRight data)
        {
            return new BODataProcessResult();
        }
    }
}
