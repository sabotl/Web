using APIWebSite.src.Context;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebSiteClassLibrary.Models;

namespace APIWebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly WebSiteClassLibrary.Interfaces.Services.IUserService _userService;
        public UserController(WebSiteClassLibrary.Interfaces.Services.IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> auth(WebSiteClassLibrary.DTO.UserDTO user)
        {
            try
            {
                var token = await _userService.AuthorizeAsync(user);
                var response = new
                {
                    access_token = token.AccessToken,
                    refreshToken = token.RefreshToken,
                    username = user.login
                };
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("refresh"), Authorize]
        public async Task<IActionResult> refresh(WebSiteClassLibrary.DTO.TokenRequest request)
        {
            try
            {
                var newToken = await _userService.refreshToken(request);

                return Ok(new
                {
                    AccessToken = newToken.AccessToken,
                    RefreshToken = newToken.RefreshToken,
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAcc(WebSiteClassLibrary.DTO.UserDTO user)
        {
            try
            {
                await _userService.CreateAccountAsync(user);
                return Ok();
            }
            catch(Exception ex) {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(innerExceptionMessage);
            }
        }
        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> UpdateAcc(WebSiteClassLibrary.Models.User user)
        {
            try
            {
                await _userService.UpdateAccountAsync(user);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllUsers"), Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("GetByLogin"), Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            try
            {
                return Json(await _userService.GetUserByLoginAsync(login));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> UserProfile()
        {
            try
            {
                return Json(await _userService.GetUserByLoginAsync(User.FindFirst(ClaimTypes.Name)?.Value));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
