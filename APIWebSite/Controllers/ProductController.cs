using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIWebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly WebSiteClassLibrary.Interfaces.Services.IProductService _productService;

        public  ProductController(WebSiteClassLibrary.Interfaces.Services.IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts() {

            try
            {
                return Json(await _productService.GetAllProductsAsync());
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("Create"), Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> Create(WebSiteClassLibrary.DTO.ProductDTO product)
        {
            try
            {
                await _productService.AddProductAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(WebSiteClassLibrary.DTO.ProductDTO productDTO)
        {
            try
            {
                await _productService.UpdateProductAsync(productDTO);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteProduct"), Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _productService.GetByCategoryAsync(category);
                return Json(products);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
