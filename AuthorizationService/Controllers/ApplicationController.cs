﻿using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public BODataProcessResult GetDatas()
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
            }
            return processResult;
        }

        [Route("GetApplicationByID")]
        [HttpPost]
        public BODataProcessResult GetData(int ID)
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
            }
            return processResult;
        }

        [Route("GetApplicationByNumber")]
        [HttpPost]
        public BODataProcessResult GetData(string Number)
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
            }
            return processResult;
        }

        [Route("CreateApplication")]
        [HttpPost]
        public async Task<BODataProcessResult> Create(BaseApplication data)
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
            return await applicationService.Create(data);
        }

        [Route("UpdateApplication")]
        [HttpPost]
        public async Task<BODataProcessResult> Update(BaseApplication data)
        {
            return await applicationService.Update(data);
        }

        [Route("DeleteApplication")]
        [HttpPost]
        public BODataProcessResult Delete(BaseApplication data)
        {
            return applicationService.Delete(data);
        }

        [Route("MarkDeletaApplication")]
        [HttpPost]
        public BODataProcessResult MarkDelete(BaseApplication data)
        {
            return applicationService.MarkDelete(data);
        }

    }
}
