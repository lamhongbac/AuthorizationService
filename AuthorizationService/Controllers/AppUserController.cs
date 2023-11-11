using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Models;
using SharedLib.Services;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        AppUserService service;
        public AppUserController(AppUserService service)
        {
            this.service = service;
        }
        [Route("GetAppUsers")]
        [HttpPost]
        public IActionResult GetDatas(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                int companyAppID = model.ID;
                List<BaseAppUser> baseDatas = service.GetDatas(companyAppID, out errMessage, out result);
                if (result == true)
                {
                    PageDataService<BaseAppUser> pageData = new PageDataService<BaseAppUser>();
                    baseDatas = pageData.GetData(baseDatas, model.PageIndex, model.PageSize);
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

        [Route("GetAppUserByID")]
        [HttpPost]
        public IActionResult GetDataByID(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppUser baseData = service.GetData(model.ID, out errMessage, out result);
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

        [Route("GetAppUserByNumber")]
        [HttpPost]
        public IActionResult GetDataByNumber(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppUser baseData = service.GetData(model.Number, model.ID, out errMessage, out result);
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

        [Route("CreateAppUser")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseAppUser data)
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

        [Route("UpdateAppUser")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseAppUser data)
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

        [Route("DeleteAppUser")]
        [HttpPost]
        public IActionResult Delete(BaseAppUser data)
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

        [Route("MarkDeleteAppUser")]
        [HttpPost]
        public async Task<IActionResult> MarkDelete(BaseAppUser data)
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

        [Route("AdminChangePass")]
        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePwdModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.AdminChangePass(model);
            }
            catch (Exception ex)
            {
                processResult.OK = false;
                processResult.Message = ex.Message;
                return BadRequest(processResult);
            }
            return Ok(processResult);
        }
    }
}
