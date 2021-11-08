using API.ActionFilters;
using BLL.Services.IServices;
using Core.Constants;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [SwaggerOperation(Summary = "roles list")]
        /// <summary>
        /// roles list 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int pageIndex = Convert.ToInt32(HttpContext.Request.Headers["PageIndex"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Headers["PageSize"]);
                return Ok(await _roleService.Get(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "get role by id")]
        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> Get(int roleId)
        {
            try
            {
                return Ok(await _roleService.Get(roleId));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "create new role")]
        /// <summary>
        /// create new role
        /// </summary>
        /// <param name="roleToAddDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleToAddDTO roleToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new SuccessDataResult<Result>(Messages.InvalidModel));
                }
                return Ok(await _roleService.Add(roleToAddDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "update role")]
        /// <summary>
        /// update role
        /// </summary>
        /// <param name="roleToUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleToUpdateDTO roleToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new SuccessDataResult<Result>(Messages.InvalidModel));
                }
                return Ok(await _roleService.Update(roleToUpdateDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "delete role by id")]
        /// <summary>
        /// delete role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            try
            {
                await _roleService.Delete(roleId);
                return Ok(new SuccessDataResult<Result>());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }
    }
}
