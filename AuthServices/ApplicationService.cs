using AuthenticationDAL;
using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;

using AutoMapper;
using SharedLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AuthServices
{
    public class ApplicationService
    {
        private string connectionString = string.Empty;
        private string tableName = "Applications";
        IMapper mapper;
        public ApplicationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ApplicationService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<BaseApplication> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<ApplicationUI> dataPortal = new GenericDataPortal<ApplicationUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<ApplicationUI> applicationUIs = dataPortal.ReadList(whereString).Result;
                if(applicationUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                IMappingHelper<BaseApplication, ApplicationUI> mappingHelper = new IMappingHelper<BaseApplication, ApplicationUI>();
                List<BaseApplication> baseApplications = mappingHelper.Map(applicationUIs);


                //List<BaseApplication> baseApplications = mapper.Map<List<BaseApplication>>(applicationUIs);
                result = true;
                errMessage = "Success";
                return baseApplications;

            }
            catch(Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseApplication GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<ApplicationUI> dataPortal = new GenericDataPortal<ApplicationUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                ApplicationUI applicationUI = dataPortal.Read(whereString, param).Result;
                if (applicationUI == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseApplication, ApplicationUI> mappingHelper = new IMappingHelper<BaseApplication, ApplicationUI>();
                BaseApplication baseApplication = mappingHelper.Map(applicationUI);

                //BaseApplication baseApplication = mapper.Map<BaseApplication>(applicationUI);
                result = true;
                errMessage = "Success";
                return baseApplication;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseApplication GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<ApplicationUI> dataPortal = new GenericDataPortal<ApplicationUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                ApplicationUI applicationUI = dataPortal.Read(whereString, param).Result;
                if (applicationUI == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseApplication, ApplicationUI> mappingHelper = new IMappingHelper<BaseApplication, ApplicationUI>();
                BaseApplication baseApplication = mappingHelper.Map(applicationUI);

                //BaseApplication baseApplication = mapper.Map<BaseApplication>(applicationUIs);
                result = true;
                errMessage = "Success";
                return baseApplication;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseApplication data)
        {
            GenericDataPortal<ApplicationUI> dataPortal = new GenericDataPortal<ApplicationUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<ApplicationUI, BaseApplication> mappingHelper = new IMappingHelper<ApplicationUI, BaseApplication>();
                ApplicationUI applicationUI = mappingHelper.Map(data);

                //ApplicationUI applicationUI = mapper.Map<ApplicationUI>(data);
                var result = await dataPortal.InsertAsync(applicationUI, null);
                if(result == true)
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

        private ApplicationUI ConvertToData(BaseApplication data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseApplication data)
        {
            GenericDataPortal<ApplicationUI> dataPortal = new GenericDataPortal<ApplicationUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<ApplicationUI, BaseApplication> mappingHelper = new IMappingHelper<ApplicationUI, BaseApplication>();
                ApplicationUI applicationUI = mappingHelper.Map(data);

                //ApplicationUI applicationUI = mapper.Map<ApplicationUI>(data);
                var result = await dataPortal.UpdateAsync(applicationUI, null);
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
        public BODataProcessResult Delete(BaseApplication data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseApplication data)
        {
            return new BODataProcessResult();
        }
    }
}
