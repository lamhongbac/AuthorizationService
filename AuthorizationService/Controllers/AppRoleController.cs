using AuthorizationService.BaseObjects;
using AuthServices;
using Microsoft.AspNetCore.Mvc;
using SharedLib;
using SharedLib.BaseObjects.Checklist;
using SharedLib.Models;
using SharedLib.Services;
using System.Data.SqlClient;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        AppRoleService service;
        public AppRoleController(AppRoleService service)
        {
            this.service = service;
        }
        [Route("GetAppRoles")]
        [HttpPost]
        public IActionResult GetDatas(AppRoleRequestDatasModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                List<BaseAppRole> baseDatas = service.GetDatas(out errMessage, out result);
                if (result == true)
                {
                    baseDatas = baseDatas.Where(x => x.CompanyAppID == model.CompanyAppID).ToList();
                    if (model.PageIndex != 0 && model.PageSize != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(model.SearchText))
                        {
                            model.SearchText = model.SearchText.ToLower();
                            baseDatas = baseDatas.Where(x => x.Number.ToLower().Contains(model.SearchText)
                            || x.Name.ToLower().Contains(model.SearchText)).ToList();
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


                        PageDataService<BaseAppRole> pageData = new PageDataService<BaseAppRole>();
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

        //[Route("GetAppRoleByID")]
        //[HttpPost]
        //public IActionResult GetData(int ID)
        //{
        //    BODataProcessResult processResult = new BODataProcessResult();
        //    string errMessage = string.Empty;
        //    bool result = false;
        //    try
        //    {
        //        BaseAppRole baseData = service.GetData(ID, out errMessage, out result);
        //        if (result == true)
        //        {
        //            processResult.Content = baseData;
        //        }
        //        processResult.OK = result;
        //        processResult.Message = errMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        processResult.OK = false;
        //        processResult.Message = ex.Message;
        //        return BadRequest(processResult);
        //    }
        //    return Ok(processResult);
        //}

        [Route("GetAppRoleByNumber")]
        [HttpPost]
        public IActionResult GetData(RequestModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string errMessage = string.Empty;
            bool result = false;
            try
            {
                BaseAppRole baseData = service.GetData(model.Number, model.ID, out errMessage, out result);
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

        [Route("CreateAppRole")]
        [HttpPost]
        public async Task<IActionResult> Create(BaseAppRole data)
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

        [Route("UpdateAppRole")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseAppRole data)
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

        [Route("DeleteAppRole")]
        [HttpPost]
        public async Task<IActionResult> Delete(BaseAppRole data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            try
            {
                processResult = await service.Delete(data);
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

        [Route("MarkDeletaAppRole")]
        [HttpPost]
        public async Task<IActionResult> MarkDelete(BaseAppRole data)
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

        private List<BaseAppRole> DoSort(List<BaseAppRole> items, string SortProperty, ESortOrder sortOrder)
        {
            try
            {
                if (SortProperty.ToLower() == "number")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.Number).ThenByDescending(x => x.ModifiedOn).ToList();
                    else
                        items = items.OrderByDescending(n => n.Number).ThenByDescending(x => x.ModifiedOn).ToList();
                }
                if (SortProperty.ToLower() == "name")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ThenByDescending(x => x.ModifiedOn).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ThenByDescending(x => x.ModifiedOn).ToList();
                }
                if (SortProperty.ToLower() == "description")
                {
                    if (sortOrder == ESortOrder.Ascending)
                        items = items.OrderBy(n => n.Description).ThenByDescending(x => x.ModifiedOn).ToList();
                    else
                        items = items.OrderByDescending(n => n.Description).ThenByDescending(x => x.ModifiedOn).ToList();
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
