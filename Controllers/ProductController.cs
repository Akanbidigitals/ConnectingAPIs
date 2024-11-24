using ConnectingAPIs.DTO;
using ConnectingAPIs.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ConnectingAPIs.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _ctx;
        public ProductController(IProduct ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO _add)
        {
            var res = await _ctx.AddProduct(_add);
            if(res.IsSuccessful)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {

            var res = await _ctx.GetAllProducts();
            return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetProductByID")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            try
            {
                var res = await _ctx.GetProductByID(id);
                return Ok(res);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct (UpdateProductDTO _update)
        {
            var res = await _ctx.UpdateProduct(_update);
            if (res.IsSuccessful)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var res = await _ctx.DeleteProduct(id);
            if (res.IsSuccessful)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

    }
}
