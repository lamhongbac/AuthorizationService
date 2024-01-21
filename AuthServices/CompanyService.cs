using AuthenticationDAL.DTO;
using AuthenticationDAL;
using AuthorizationService.BaseObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MSASharedLib.Utils;
using SharedLib.Authentication;
using AuthorizationService.DataTypes;
using Microsoft.Extensions.Configuration;
using MSASharedLib.DataTypes;

namespace AuthServices
{
    public class CompanyService
    {
        private string connectionString = string.Empty;
        private string tableName = "Companies";
        IMapper mapper;
        
        public CompanyService(IConfiguration configuration, IMapper mapper)
        {
            var configSection = configuration.GetSection("AppConfig");
            AppConfiguration appConfig = configSection.Get<AppConfiguration>();
            this.connectionString = configuration.GetConnectionString(appConfig.ProductMode);
            this.mapper = mapper;
        }
        public List<BaseCompany> GetDatas(out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyUI> dataPortal = new GenericDataPortal<CompanyUI>(connectionString, tableName);
                string whereString = string.Empty;
                List<CompanyUI> CompanyUIs = dataPortal.ReadList(whereString).Result;
                if (CompanyUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                //IMappingHelper<BaseCompany, CompanyUI> mappingHelper = new IMappingHelper<BaseCompany, CompanyUI>();
                //List<BaseCompany> BaseCompanys = mappingHelper.Map(CompanyUIs);

                List<BaseCompany> BaseCompanys = mapper.Map<List<BaseCompany>>(CompanyUIs);
                result = true;
                errMessage = "Success";
                return BaseCompanys;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseCompany GetData(int ID, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyUI> dataPortal = new GenericDataPortal<CompanyUI>(connectionString, tableName);
                string whereString = "ID = @ID";
                object param = new { ID = ID };
                CompanyUI CompanyUIs = dataPortal.Read(whereString, param).Result;
                if (CompanyUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }
                //IMappingHelper<BaseCompany, CompanyUI> mappingHelper = new IMappingHelper<BaseCompany, CompanyUI>();
                //BaseCompany BaseCompany = mappingHelper.Map(CompanyUIs);

                BaseCompany BaseCompany = mapper.Map<BaseCompany>(CompanyUIs);
                result = true;
                errMessage = "Success";
                return BaseCompany;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public BaseCompany GetData(string Number, out string errMessage, out bool result)
        {
            try
            {
                GenericDataPortal<CompanyUI> dataPortal = new GenericDataPortal<CompanyUI>(connectionString, tableName);
                string whereString = "Number = @Number";
                object param = new { Number = Number };
                CompanyUI CompanyUIs = dataPortal.Read(whereString, param).Result;
                if (CompanyUIs == null)
                {
                    result = false;
                    errMessage = "Data not Found";
                    return null;
                }

                //IMappingHelper<BaseCompany, CompanyUI> mappingHelper = new IMappingHelper<BaseCompany, CompanyUI>();
                //BaseCompany BaseCompany = mappingHelper.Map(CompanyUIs);

                BaseCompany BaseCompany = mapper.Map<BaseCompany>(CompanyUIs);
                result = true;
                errMessage = "Success";
                return BaseCompany;

            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BODataProcessResult> Create(BaseCompany data)
        {
            GenericDataPortal<CompanyUI> dataPortal = new GenericDataPortal<CompanyUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                //IMappingHelper<CompanyUI, BaseCompany> mappingHelper = new IMappingHelper<CompanyUI, BaseCompany>();
                //CompanyUI CompanyUI = mappingHelper.Map(data);

                CompanyUI CompanyUI = mapper.Map<CompanyUI>(data);
                var result = await dataPortal.InsertAsync(CompanyUI, null);
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

        private CompanyUI ConvertToData(BaseCompany data)
        {
            throw new NotImplementedException();
        }

        public async Task<BODataProcessResult> Update(BaseCompany data)
        {
            GenericDataPortal<CompanyUI> dataPortal = new GenericDataPortal<CompanyUI>(connectionString, tableName);
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                //IMappingHelper<CompanyUI, BaseCompany> mappingHelper = new IMappingHelper<CompanyUI, BaseCompany>();
                //CompanyUI CompanyUI = mappingHelper.Map(data);

                CompanyUI CompanyUI = mapper.Map<CompanyUI>(data);
                var result = await dataPortal.UpdateAsync(CompanyUI, null);
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
        public BODataProcessResult Delete(BaseCompany data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseCompany data)
        {
            return new BODataProcessResult();
        }
    }
}
