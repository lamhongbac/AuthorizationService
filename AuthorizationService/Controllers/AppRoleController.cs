using AuthorizationService.BaseObjects;
using AuthorizationService.Models;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        AppRoleService service;
        public AppRoleController(AppRoleService service)
        {
            this.service = service;
        }
        [Route("GetAppRoles")]
        [HttpPost]
        public IActionResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseAppRole> baseDatas = service.GetDatas(out errMessage, out result);
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

        //[Route("GetAppRoleByID")]
        //[HttpPost]
        //public IActionResult GetData(int ID)
        //{
        //    BODataProcessResult processResult = new BODataProcessResult();
        //    string errMessage = string.Empty;
        //    bool result = false;
        //    try
        //    {
        //        BaseAppRole baseData = service.GetData(ID, out errMessage, out result);
        //        if (result == true)
        //        {
        //            processResult.Content = baseData;
        //        }
        //        processResult.OK = result;
        //        processResult.Message = errMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        processResult.OK = false;
        //        processResult.Message = ex.Message;
        //        return BadRequest(processResult);
        //    }
        //    return Ok(processResult);
        //}

        [Route("GetAppRoleByNumber")]
        [HttpPost]
        public IActionResult GetData(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppRole baseData = service.GetData(model.Number, out errMessage, out result);
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

        [Route("CreateAppRole")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseAppRole data)
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

        [Route("UpdateAppRole")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseAppRole data)
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

        [Route("DeleteAppRole")]
        [HttpPost]
        public async Task<IActionResult> Delete(BaseAppRole data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.Delete(data);
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

        [Route("MarkDeletaAppRole")]
        [HttpPost]
        public async Task<IActionResult> MarkDelete(BaseAppRole data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.MarkDelete(data);
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
