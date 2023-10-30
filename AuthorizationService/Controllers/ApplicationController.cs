using AuthenticationDAL.DTO;
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
        public async Task<IActionResult> Create(BaseApplication data)
        {
            BODataProcessResult processResult=new BODataProcessResult();
            try
            {
                 processResult =await applicationService.Create(data);
            }
            catch
            {

            }
            finally
            {

            }
            return Ok(processResult);
        }
        public BODataProcessResult Update(BaseApplication data)
        {
            return applicationService.Create(data);
        }
        public BODataProcessResult Delete(BaseApplication data)
        {
            return applicationService.Create(data);
        }
        public BODataProcessResult MarkDelete(BaseApplication data)
        {
            return applicationService.Create(data);
        }

    }
}
