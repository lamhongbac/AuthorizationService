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
    public class AppObjectService
    {
        private string connectionString = string.Empty;
        private string tableName = "AppObjects";
        IMapper mapper;
        public AppObjectService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<BaseAppObject> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<AppObjectUI> AppObjectUIs = dataPortal.ReadList(whereString).Result;
                if (AppObjectUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                List<BaseAppObject> BaseAppObjects = mapper.Map<List<BaseAppObject>>(AppObjectUIs);
                result = true;
                errMessage = "Success";
                return BaseAppObjects;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppObject GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                AppObjectUI AppObjectUIs = dataPortal.Read(whereString, param).Result;
                if (AppObjectUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseAppObject BaseAppObject = mapper.Map<BaseAppObject>(AppObjectUIs);
                result = true;
                errMessage = "Success";
                return BaseAppObject;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseAppObject GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                AppObjectUI AppObjectUIs = dataPortal.Read(whereString, param).Result;
                if (AppObjectUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                BaseAppObject BaseAppObject = mapper.Map<BaseAppObject>(AppObjectUIs);
                result = true;
                errMessage = "Success";
                return BaseAppObject;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseAppObject data)
        {
            GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppObjectUI AppObjectUI = mapper.Map<AppObjectUI>(data);
                var result = await dataPortal.InsertAsync(AppObjectUI, null);
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

        private AppObjectUI ConvertToData(BaseAppObject data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseAppObject data)
        {
            GenericDataPortal<AppObjectUI> dataPortal = new GenericDataPortal<AppObjectUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                AppObjectUI AppObjectUI = mapper.Map<AppObjectUI>(data);
                var result = await dataPortal.UpdateAsync(AppObjectUI, null);
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
        public BODataProcessResult Delete(BaseAppObject data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseAppObject data)
        {
            return new BODataProcessResult();
        }
    }
}
