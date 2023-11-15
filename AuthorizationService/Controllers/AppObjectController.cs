using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Models;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppObjectController : ControllerBase
    {
        AppObjectService service;
        CompanyApplicationService companyApplicationService;
        public AppObjectController(AppObjectService service, CompanyApplicationService companyApplicationService)
        {
            this.service = service;
            this.companyApplicationService = companyApplicationService;

        }
        [Route("GetAppObjects")]
        [HttpPost]
        public IActionResult GetDatas(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompanyApplication baseCompany = companyApplicationService.GetData(model.ID, out errMessage, out result);
                if(baseCompany == null)
                {
                    processResult.OK = result;
                    processResult.Message = errMessage;
                    return Ok(processResult);
                }
                List<BaseAppObject> baseDatas = service.GetDatas(out errMessage, out result);
                if (result == true)
                {
                    baseDatas = baseDatas.Where(x => x.AppID == baseCompany.AppID).ToList();
                    processResult.Content = baseDatas;
                }
                processResult.OK = result;
                processResult.Message = errMessage;
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }

        [Route("GetAppObjectByID")]
        [HttpPost]
        public IActionResult GetData(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppObject baseData = service.GetData(model.ID, out errMessage, out result);
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
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }

        [Route("GetAppObjectByNumber")]
        [HttpPost]
        public IActionResult GetDataByNumber(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompanyApplication baseCompany = companyApplicationService.GetData(model.ID, out errMessage, out result);
                if (baseCompany == null)
                {
                    processResult.OK = result;
                    processResult.Message = errMessage;
                    return Ok(processResult);
                }
                BaseAppObject baseData = service.GetData(model.Number, baseCompany.AppID, out errMessage, out result);
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
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }

        [Route("CreateAppObject")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseAppObject data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.Create(data);
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            finally
            {

            }
            return Ok(processResult);
        }

        [Route("UpdateAppObject")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseAppObject data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.Update(data);
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            finally
            {

            }
            return Ok(processResult);
        }

        [Route("DeleteAppObject")]
        [HttpPost]
        public IActionResult Delete(BaseAppObject data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = service.Delete(data);
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            finally
            {

            }
            return Ok(processResult);
        }

        [Route("MarkDeletaAppObject")]
        [HttpPost]
        public IActionResult MarkDelete(BaseAppObject data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = service.MarkDelete(data);
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            finally
            {

            }
            return Ok(processResult);
        }
    }
}
