using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

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
        public BODataProcessResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseAppUser> baseDatas = service.GetDatas(out errMessage, out result);
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

        [Route("GetAppUserByID")]
        [HttpPost]
        public BODataProcessResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppUser baseData = service.GetData(ID, out errMessage, out result);
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

        [Route("GetAppUserByNumber")]
        [HttpPost]
        public BODataProcessResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppUser baseData = service.GetData(Number, out errMessage, out result);
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

        [Route("CreateAppUser")]
        [HttpPost]
        public async Task<BODataProcessResult> Create(BaseAppUser data)
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

        [Route("UpdateAppUser")]
        [HttpPost]
        public async Task<BODataProcessResult> Update(BaseAppUser data)
        {
            return await service.Update(data);
        }

        [Route("DeleteAppUser")]
        [HttpPost]
        public BODataProcessResult Delete(BaseAppUser data)
        {
            return service.Delete(data);
        }

        [Route("MarkDeletaAppUser")]
        [HttpPost]
        public BODataProcessResult MarkDelete(BaseAppUser data)
        {
            return service.MarkDelete(data);
        }
    }
}
