using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Utils;

namespace AuthorizationService.Controllers
{
    //controller-> logic-> dal->end
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        ApplicationService applicationService;
        public ApplicationController(ApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [Route("GetApplications")]
        [HttpPost]
        public IActionResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseApplication> baseApplications = applicationService.GetDatas(out errMessage, out result);
                if(result == true)
                {
                    processResult.Content = baseApplications;
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

        [Route("GetApplicationByID")]
        [HttpPost]
        public IActionResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseApplication baseApplication = applicationService.GetData(ID, out errMessage, out result);
                if (result == true)
                {
                    processResult.Content = baseApplication;
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

        [Route("GetApplicationByNumber")]
        [HttpPost]
        public IActionResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseApplication baseApplication = applicationService.GetData(Number, out errMessage, out result);
                if (result == true)
                {
                    processResult.Content = baseApplication;
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

        [Route("CreateApplication")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseApplication data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await applicationService.Create(data);
            }
            catch(Exception ex)
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

        [Route("UpdateApplication")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseApplication data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await applicationService.Update(data);
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

        [Route("DeleteApplication")]
        [HttpPost]
        public IActionResult Delete(BaseApplication data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = applicationService.Delete(data);
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

        [Route("MarkDeletaApplication")]
        [HttpPost]
        public IActionResult MarkDelete(BaseApplication data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = applicationService.MarkDelete(data);
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
