using api.MiniCatalogo.Config.Const;
using System.ComponentModel.DataAnnotations;

namespace api.MiniCatalogo.DTOs.Request
{
    public class SearchListProduct
    {
        /// <summary>
        /// Page number for pagination.
        /// </summary>
        [Required(ErrorMessage = Messages._requiredPage), Range(1, int.MaxValue, ErrorMessage = Messages._minimunPage)]
        public int Page { get; set; }
        /// <summary>
        /// Number of items per page.
        /// </summary>
        [Required(ErrorMessage = Messages._requiredSize), Range(1, int.MaxValue, ErrorMessage = Messages._minimunSize)]
        public int Size { get; set; }
        /// <summary>
        /// Product category.
        /// </summary>
        [Required(ErrorMessage = Messages._requiredIdCategori), Range(1, int.MaxValue, ErrorMessage = Messages._requiredIdCategori)]
        public int CategoriaId { get; set; }
    }
}
