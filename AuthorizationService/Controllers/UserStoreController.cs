using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoreController : Controller
    {
        UserRoleService service;
        public UserStoreController(UserRoleService service)
        {
            this.service = service;
        }
        [Route("GetUserRoles")]
        [HttpPost]
        public BODataProcessResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseUserRole> baseDatas = service.GetDatas(out errMessage, out result);
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

        [Route("GetUserRoleByID")]
        [HttpPost]
        public BODataProcessResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseUserRole baseData = service.GetData(ID, out errMessage, out result);
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

        [Route("GetUserRoleByNumber")]
        [HttpPost]
        public BODataProcessResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseUserRole baseData = service.GetData(Number, out errMessage, out result);
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

        [Route("CreateUserRole")]
        [HttpPost]
        public async Task<BODataProcessResult> Create(BaseUserRole data)
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

        [Route("UpdateUserRole")]
        [HttpPost]
        public async Task<BODataProcessResult> Update(BaseUserRole data)
        {
            return await service.Update(data);
        }

        [Route("DeleteUserRole")]
        [HttpPost]
        public BODataProcessResult Delete(BaseUserRole data)
        {
            return service.Delete(data);
        }

        [Route("MarkDeletaUserRole")]
        [HttpPost]
        public BODataProcessResult MarkDelete(BaseUserRole data)
        {
            return service.MarkDelete(data);
        }
    }
}
