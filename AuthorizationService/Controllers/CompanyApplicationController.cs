using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyApplicationController : ControllerBase
    {
        CompanyApplicationService service;
        public CompanyApplicationController(CompanyApplicationService service)
        {
            this.service = service;
        }
        [Route("GetCompanyApplications")]
        [HttpPost]
        public BODataProcessResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseCompanyApplication> baseDatas = service.GetDatas(out errMessage, out result);
                if (result == true)
                {
                    processResult.Content = baseDatas;
                }
                processResult.OK = result;
                processResult.Message = errMessage;
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
            }
            return processResult;
        }

        [Route("GetCompanyApplicationByID")]
        [HttpPost]
        public BODataProcessResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompanyApplication baseData = service.GetData(ID, out errMessage, out result);
                if (result == true)
                {
                    processResult.Content = baseData;
                }
                processResult.OK = result;
                processResult.Message = errMessage;
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
            }
            return processResult;
        }

        [Route("GetCompanyApplicationByNumber")]
        [HttpPost]
        public BODataProcessResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompanyApplication baseData = service.GetData(Number, out errMessage, out result);
                if (result == true)
                {
                    processResult.Content = baseData;
                }
                processResult.OK = result;
                processResult.Message = errMessage;
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
            }
            return processResult;
        }

        [Route("CreateCompanyApplication")]
        [HttpPost]
        public async Task<BODataProcessResult> Create(BaseCompanyApplication data)
        {
            //BODataProcessResult processResult=new BODataProcessResult();
            //try
            //{
            //     processResult =await applicationService.Create(data);
            //}
            //catch
            //{

            //}
            //finally
            //{

            //}
            //return Ok(processResult);
            return await service.Create(data);
        }

        [Route("UpdateCompanyApplication")]
        [HttpPost]
        public async Task<BODataProcessResult> Update(BaseCompanyApplication data)
        {
            return await service.Update(data);
        }

        [Route("DeleteCompanyApplication")]
        [HttpPost]
        public BODataProcessResult Delete(BaseCompanyApplication data)
        {
            return service.Delete(data);
        }

        [Route("MarkDeletaCompanyApplication")]
        [HttpPost]
        public BODataProcessResult MarkDelete(BaseCompanyApplication data)
        {
            return service.MarkDelete(data);
        }
    }
}
