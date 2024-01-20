using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLib;
using SharedLib.Utils;

//using SharedLib.Models;

using System.Data.SqlClient;

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
        public IActionResult GetDatas(AppUserRequestDatasModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                
                List<BaseAppUser> baseDatas = service.GetDatas(model.CompanyAppID, out errMessage, out result);
                if (result == true)
                {
                    baseDatas = baseDatas.Where(x => x.IsDeleted == false).ToList();
                    if (model.PageIndex != 0 && model.PageSize != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(model.SearchText))
                        {
                            model.SearchText = model.SearchText.ToLower();
                            baseDatas = baseDatas.Where(x => x.UserName.ToLower().Contains(model.SearchText)
                            || x.FullName.ToLower().Contains(model.SearchText)).ToList();
                        }

                        ESortOrder eSortOrder = ESortOrder.Ascending;
                        if (!string.IsNullOrWhiteSpace(model.sortOrder))
                        {
                            try
                            {
                                eSortOrder = Enum.Parse<ESortOrder>(model.sortOrder);
                            }
                            catch
                            {

                            }
                        }

                        baseDatas = DoSort(baseDatas, model.SortProperty, eSortOrder);
                        int totalCount = baseDatas.Count;
                        processResult.ErrorNumber = totalCount;
                        PageDataService <BaseAppUser> pageData = new PageDataService<BaseAppUser>();
                        baseDatas = pageData.GetData(baseDatas, model.PageIndex, model.PageSize);
                    }
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

        [Route("GetAppUsersByDepartment")]
        [HttpPost]
        public IActionResult GetDatasByDepartment(AppUserRequestDatasModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseAppUser> baseDatas = service.GetDatas(model.CompanyAppID, model.Department, out errMessage, out result);
                if(baseDatas != null)
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

        [Route("GetAppUsersByManager")]
        [HttpPost]
        public IActionResult GetDatasByManager(AppUserRequestDatasModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseAppUser> baseDatas = service.GetDatas(model.CompanyAppID, model.Department, model.ManagerID, out errMessage, out result);
                if (baseDatas != null)
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

        private List<BaseAppUser> DoSort(List<BaseAppUser> items, string SortProperty, ESortOrder sortOrder)
        {
            try
            {
                if (SortProperty.ToLower() == "id")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.ID).ToList();
                    else
                        items = items.OrderByDescending(n => n.ID).ToList();
                }
                if (SortProperty.ToLower() == "number")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.UserName).ToList();
                    else
                        items = items.OrderByDescending(n => n.UserName).ToList();
                }
                if (SortProperty.ToLower() == "name")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.FullName).ToList();
                    else
                        items = items.OrderByDescending(n => n.FullName).ToList();
                }
                if (SortProperty.ToLower() == "modifiedby")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.ModifiedBy).ThenByDescending(x => x.ModifiedOn).ToList();
                    else
                        items = items.OrderByDescending(n => n.ModifiedBy).ThenByDescending(x => x.ModifiedOn).ToList();
                }
                if (SortProperty.ToLower() == "modifiedon")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.ModifiedOn).ToList();
                    else
                        items = items.OrderByDescending(n => n.ModifiedOn).ToList();
                }
                return items;
            }
            catch
            {
                throw;
            }
        }
    }
}
