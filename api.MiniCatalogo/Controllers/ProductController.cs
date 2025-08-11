using api.MiniCatalogo.Repository.Product;
using api.MiniCatalogo.DTOs.Response;
using api.MiniCatalogo.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using api.MiniCatalogo.Model;

namespace api.MiniCatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        readonly OperationProduct _operationProduct;
        readonly SearchProduct _searchProduct;
        public ProductController(OperationProduct operationProduct, SearchProduct searchProduct)
        {
            _operationProduct = operationProduct;
            _searchProduct = searchProduct;
        }
        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="searchListProduct"></param>
        /// <returns></returns>
        /// <remarks>
        /// Retrieves a paginated list of products.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(ProdutoResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcepitioResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProdutoResponseDTO>>> Get([FromQuery] SearchListProduct searchListProduct)
        {
            try
            {
                return Ok(await _searchProduct.GetListPages(searchListProduct));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExcepitioResponse { Detail = ex.InnerException?.Message });
            }
        }
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Creates a new product.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcepitioResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(ProdutoRequestDTO value)
        {
            try
            {
                await _operationProduct.AddAsync(value);
                
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(new ExcepitioResponse { Detail = ex.InnerException?.Message });
            }
        }
    }
}
