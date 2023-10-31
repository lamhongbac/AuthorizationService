using AuthorizationService.Service;
using AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        UserProfileService _userProfileService;
        public UserProfileController(  UserProfileService userProfileService)
        {
            
            _userProfileService = userProfileService;
        }
        public IActionResult ChangePwd(ChangePwdModel model)
        {

            var changePwdResult = _userProfileService.ChangePwd(model);
            return Ok(changePwdResult);
        }
        public IActionResult UpdateProfile(ChangePwdModel model)
        {

            var updateProfileResult = _userProfileService.UpdateProfile(model);
            return Ok(updateProfileResult);
        }
    }
}
