using System;
using System.Threading.Tasks;
using API.ActionFilters;
using BLL.Services.IServices;
using Core.Constants;
using DAL.UnitOfWorks.IUnitOfWorks;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [SwaggerOperation(Summary = "users list")]
        /// <summary>
        /// users list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int pageIndex = Convert.ToInt32(HttpContext.Request.Headers["PageIndex"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Headers["PageSize"]);
                return Ok(await _userService.Get(pageIndex, pageSize));
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "get user by id")]
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                return Ok(await _userService.Get(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "create new user")]
        /// <summary>
        /// create new user
        /// </summary>
        /// <param name="userToAddDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserToAddDTO userToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new SuccessDataResult<Result>(Messages.InvalidModel));
                }
                return Ok(await _userService.Add(userToAddDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "update user")]
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="userToUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserToUpdateDTO userToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new SuccessDataResult<Result>(Messages.InvalidModel));
                }
                return Ok(await _userService.Update(userToUpdateDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }

        [SwaggerOperation(Summary = "delete user by id")]
        /// <summary>
        /// delete user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await _userService.Delete(userId);
                return Ok(new SuccessDataResult<Result>());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        }
    }
}
