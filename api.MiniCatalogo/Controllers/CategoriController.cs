using api.MiniCatalogo.Repository.Categori;
using api.MiniCatalogo.DTOs.Response;
using api.MiniCatalogo.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using api.MiniCatalogo.Model;

namespace api.MiniCatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriController : ControllerBase
    {
        readonly SearchCategori _searchCategori;
        readonly OperationCategori _operationCategori;
        public CategoriController(SearchCategori searchCategori, OperationCategori operationCategori)
        {
            _searchCategori = searchCategori;
            _operationCategori = operationCategori;
        }

        /// <summary>
        /// Returns a list of all categories registered in the system.
        /// </summary>
        /// <remarks>
        /// Returns a list of all categories registered in the system.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcepitioResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoriaResponseDTO>>> Get()
        {
            try
            {
                return Ok(await _searchCategori.GetAllAsync());
            }catch (Exception ex)
            {
                return BadRequest(new ExcepitioResponse { Detail = ex.InnerException?.Message });
            }
        }

        /// <summary>
        /// Creates a new category in the system.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// Creates a new category in the system.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcepitioResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CategoriaRequestDTO value)
        {
            try
            {
                await _operationCategori.AddAsync(value);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExcepitioResponse { Detail = ex.InnerException?.Message });
            }
        }
    }
}