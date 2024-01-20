using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Utils;

namespace AuthServices
{
    public class UserStoreService
    {
        private string connectionString = string.Empty;
        private string tableName = "UserStores";
        IMapper mapper;
        public UserStoreService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public UserStoreService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BaseUserStore> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserStoreUI> dataPortal = new GenericDataPortal<UserStoreUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<UserStoreUI> UserStoreUIs = dataPortal.ReadList(whereString).Result;
                if (UserStoreUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseUserStore, UserStoreUI> mappingHelper = new IMappingHelper<BaseUserStore, UserStoreUI>();
                List<BaseUserStore> BaseUserStores = mappingHelper.Map(UserStoreUIs);

                //List<BaseUserStore> BaseUserStores = mapper.Map<List<BaseUserStore>>(UserStoreUIs);
                result = true;
                errMessage = "Success";
                return BaseUserStores;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseUserStore GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserStoreUI> dataPortal = new GenericDataPortal<UserStoreUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                UserStoreUI UserStoreUIs = dataPortal.Read(whereString, param).Result;
                if (UserStoreUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseUserStore, UserStoreUI> mappingHelper = new IMappingHelper<BaseUserStore, UserStoreUI>();
                BaseUserStore BaseUserStore = mappingHelper.Map(UserStoreUIs);

                //BaseUserStore BaseUserStore = mapper.Map<BaseUserStore>(UserStoreUIs);
                result = true;
                errMessage = "Success";
                return BaseUserStore;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseUserStore GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<UserStoreUI> dataPortal = new GenericDataPortal<UserStoreUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                UserStoreUI UserStoreUIs = dataPortal.Read(whereString, param).Result;
                if (UserStoreUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                
                IMappingHelper<BaseUserStore, UserStoreUI> mappingHelper = new IMappingHelper<BaseUserStore, UserStoreUI>();
                BaseUserStore BaseUserStore = mappingHelper.Map(UserStoreUIs);

                //BaseUserStore BaseUserStore = mapper.Map<BaseUserStore>(UserStoreUIs);
                result = true;
                errMessage = "Success";
                return BaseUserStore;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseUserStore data)
        {
            GenericDataPortal<UserStoreUI> dataPortal = new GenericDataPortal<UserStoreUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<UserStoreUI, BaseUserStore> mappingHelper = new IMappingHelper<UserStoreUI, BaseUserStore>();
                UserStoreUI UserStoreUI = mappingHelper.Map(data);

                //UserStoreUI UserStoreUI = mapper.Map<UserStoreUI>(data);
                var result = await dataPortal.InsertAsync(UserStoreUI, null);
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

        private UserStoreUI ConvertToData(BaseUserStore data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseUserStore data)
        {
            GenericDataPortal<UserStoreUI> dataPortal = new GenericDataPortal<UserStoreUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<UserStoreUI, BaseUserStore> mappingHelper = new IMappingHelper<UserStoreUI, BaseUserStore>();
                UserStoreUI UserStoreUI = mappingHelper.Map(data);

                //UserStoreUI UserStoreUI = mapper.Map<UserStoreUI>(data);
                var result = await dataPortal.UpdateAsync(UserStoreUI, null);
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
        public BODataProcessResult Delete(BaseUserStore data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseUserStore data)
        {
            return new BODataProcessResult();
        }
    }
}
