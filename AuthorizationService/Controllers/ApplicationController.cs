using AuthenticationDAL.DTO;
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
        public BODataProcessResult Create(Application data)
        {
            return applicationService.Create(data);
        }
        public BODataProcessResult Update(Application data)
        {
            return applicationService.Create(data);
        }
        public BODataProcessResult Delete(Application data)
        {
            return applicationService.Create(data);
        }
        public BODataProcessResult MarkDelete(Application data)
        {
            return applicationService.Create(data);
        }

    }
}
