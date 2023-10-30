using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleRightController : ControllerBase
    {
        RoleRightService service;
        public RoleRightController(RoleRightService service)
        {
            this.service = service;
        }
        [Route("GetRoleRights")]
        [HttpPost]
        public BODataProcessResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseRoleRight> baseDatas = service.GetDatas(out errMessage, out result);
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

        [Route("GetRoleRightByID")]
        [HttpPost]
        public BODataProcessResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseRoleRight baseData = service.GetData(ID, out errMessage, out result);
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

        [Route("GetRoleRightByNumber")]
        [HttpPost]
        public BODataProcessResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseRoleRight baseData = service.GetData(Number, out errMessage, out result);
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

        [Route("CreateRoleRight")]
        [HttpPost]
        public async Task<BODataProcessResult> Create(BaseRoleRight data)
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

        [Route("UpdateRoleRight")]
        [HttpPost]
        public async Task<BODataProcessResult> Update(BaseRoleRight data)
        {
            return await service.Update(data);
        }

        [Route("DeleteRoleRight")]
        [HttpPost]
        public BODataProcessResult Delete(BaseRoleRight data)
        {
            return service.Delete(data);
        }

        [Route("MarkDeletaRoleRight")]
        [HttpPost]
        public BODataProcessResult MarkDelete(BaseRoleRight data)
        {
            return service.MarkDelete(data);
        }
    }
}
