using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public CatalogController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productAppService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _productAppService.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetProductsByIds(int[] productIds)
        {
            var products = _productAppService.GetByIds(productIds);
            return Ok(products);
        }
    }
}
