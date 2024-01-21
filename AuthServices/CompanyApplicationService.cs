using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Utils;

using SharedLib.Authentication;
using AuthorizationService.DataTypes;
using Microsoft.Extensions.Configuration;

namespace AuthServices
{
    public class CompanyApplicationService
    {
        private string connectionString = string.Empty;
        private string tableName = "CompanyApplication";
        IMapper mapper;

        public CompanyApplicationService(IConfiguration configuration, IMapper mapper)
        {
            var configSection = configuration.GetSection("AppConfig");
            AppConfiguration appConfig = configSection.Get<AppConfiguration>();
            this.connectionString = configuration.GetConnectionString(appConfig.ProductMode);
            this.mapper = mapper;
        }
        public List<BaseCompanyApplication> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyApplicationUI> dataPortal = new GenericDataPortal<CompanyApplicationUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<CompanyApplicationUI> CompanyApplicationUIs = dataPortal.ReadList(whereString).Result;
                if (CompanyApplicationUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseCompanyApplication, CompanyApplicationUI> mappingHelper = new IMappingHelper<BaseCompanyApplication, CompanyApplicationUI>();
                List<BaseCompanyApplication> BaseCompanyApplications = mappingHelper.Map(CompanyApplicationUIs);

                //List<BaseCompanyApplication> BaseCompanyApplications = mapper.Map<List<BaseCompanyApplication>>(CompanyApplicationUIs);
                result = true;
                errMessage = "Success";
                return BaseCompanyApplications;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseCompanyApplication GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyApplicationUI> dataPortal = new GenericDataPortal<CompanyApplicationUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                CompanyApplicationUI CompanyApplicationUIs = dataPortal.Read(whereString, param).Result;
                if (CompanyApplicationUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseCompanyApplication, CompanyApplicationUI> mappingHelper = new IMappingHelper<BaseCompanyApplication, CompanyApplicationUI>();
                BaseCompanyApplication BaseCompanyApplication = mappingHelper.Map(CompanyApplicationUIs);

                //BaseCompanyApplication BaseCompanyApplication = mapper.Map<BaseCompanyApplication>(CompanyApplicationUIs);
                result = true;
                errMessage = "Success";
                return BaseCompanyApplication;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseCompanyApplication GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyApplicationUI> dataPortal = new GenericDataPortal<CompanyApplicationUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                CompanyApplicationUI CompanyApplicationUIs = dataPortal.Read(whereString, param).Result;
                if (CompanyApplicationUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                IMappingHelper<BaseCompanyApplication, CompanyApplicationUI> mappingHelper = new IMappingHelper<BaseCompanyApplication, CompanyApplicationUI>();
                BaseCompanyApplication BaseCompanyApplication = mappingHelper.Map(CompanyApplicationUIs);

                //BaseCompanyApplication BaseCompanyApplication = mapper.Map<BaseCompanyApplication>(CompanyApplicationUIs);
                result = true;
                errMessage = "Success";
                return BaseCompanyApplication;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseCompanyApplication data)
        {
            GenericDataPortal<CompanyApplicationUI> dataPortal = new GenericDataPortal<CompanyApplicationUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<CompanyApplicationUI, BaseCompanyApplication> mappingHelper = new IMappingHelper<CompanyApplicationUI, BaseCompanyApplication>();
                CompanyApplicationUI CompanyApplicationUI = mappingHelper.Map(data);

                //CompanyApplicationUI CompanyApplicationUI = mapper.Map<CompanyApplicationUI>(data);
                var result = await dataPortal.InsertAsync(CompanyApplicationUI, null);
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

        private CompanyApplicationUI ConvertToData(BaseCompanyApplication data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseCompanyApplication data)
        {
            GenericDataPortal<CompanyApplicationUI> dataPortal = new GenericDataPortal<CompanyApplicationUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                IMappingHelper<CompanyApplicationUI, BaseCompanyApplication> mappingHelper = new IMappingHelper<CompanyApplicationUI, BaseCompanyApplication>();
                CompanyApplicationUI CompanyApplicationUI = mappingHelper.Map(data);

                //CompanyApplicationUI CompanyApplicationUI = mapper.Map<CompanyApplicationUI>(data);
                var result = await dataPortal.UpdateAsync(CompanyApplicationUI, null);
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
        public BODataProcessResult Delete(BaseCompanyApplication data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseCompanyApplication data)
        {
            return new BODataProcessResult();
        }
    }
}
