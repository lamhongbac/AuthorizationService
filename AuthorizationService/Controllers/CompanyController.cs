using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        CompanyService service;
        public CompanyController(CompanyService service)
        {
            this.service = service;
        }
        [Route("GetCompanys")]
        [HttpPost]
      
        public IActionResult GetDatas()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseCompany> baseDatas = service.GetDatas(out errMessage, out result);
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

        [Route("GetCompanyByID")]
        [HttpPost]
      
        public IActionResult GetData(int ID)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompany baseData = service.GetData(ID, out errMessage, out result);
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

        [Route("GetCompanyByNumber")]
        [HttpPost]
      
        public IActionResult GetData(string Number)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseCompany baseData = service.GetData(Number, out errMessage, out result);
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

        [Route("CreateCompany")]
        [HttpPost]
       
        public async Task<IActionResult> Create(BaseCompany data)
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

        [Route("UpdateCompany")]
        [HttpPost]
        
        public async Task<IActionResult> Update(BaseCompany data)
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

        [Route("DeleteCompany")]
        [HttpPost]
      
        public IActionResult Delete(BaseCompany data)
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

        [Route("MarkDeletaCompany")]
        [HttpPost]
     
        public IActionResult MarkDelete(BaseCompany data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = service.MarkDelete(data);
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
