using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebSiteClassLibrary.Interfaces.Services;

namespace APIWebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Index()
        {

                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (userId == null)
                    return Json("User claims error");

                return Json(await _cartService.Get(userId));

        }
        [HttpPost("add"), Authorize]
        public async Task<IActionResult> add(WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (userId == null)
                    return BadRequest("User claims error");

                await _cartService.Add(userId, productToCartDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("remove"), Authorize]
        public async Task<IActionResult> remove(Guid id)
        {
            try
            {
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    }
}
