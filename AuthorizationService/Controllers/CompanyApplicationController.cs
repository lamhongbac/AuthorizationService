
using AuthorizationService.BaseObjects;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Mvc;
using MSASharedLib.DataTypes;
using MSASharedLib.Utils;
//using SharedLib.Models;

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
        public IActionResult GetDatas()
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
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }

        [Route("GetCompanyApplicationByID")]
        [HttpPost]
        public IActionResult GetData(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompanyApplication baseData = service.GetData(model.ID, out errMessage, out result);
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

        [Route("GetCompanyApplicationByNumber")]
        [HttpPost]
        public IActionResult GetData(string Number)
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
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }

        [Route("CreateCompanyApplication")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseCompanyApplication data)
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

        [Route("UpdateCompanyApplication")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseCompanyApplication data)
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

        [Route("DeleteCompanyApplication")]
        [HttpPost]
        public IActionResult Delete(BaseCompanyApplication data)
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

        [Route("MarkDeletaCompanyApplication")]
        [HttpPost]
        public IActionResult MarkDelete(BaseCompanyApplication data)
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
