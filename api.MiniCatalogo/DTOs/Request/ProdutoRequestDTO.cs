using api.MiniCatalogo.Config.Const;
using System.ComponentModel.DataAnnotations;

namespace api.MiniCatalogo.DTOs.Request
{
    public class ProdutoRequestDTO
    {
        /// <summary>
        /// Product name.
        /// </summary>
        [Required(ErrorMessage = Messages._requiredName)]
        public string Nome { get; set; } = null!;
        /// <summary>
        /// Product price.
        /// </summary>
        [Required ,Range(0.01, double.MaxValue, ErrorMessage = Messages._invalidPrice)]
        public decimal Preco { get; set; }
        /// <summary>
        /// Product category.
        /// </summary>
        [Required, Range(1, int.MaxValue, ErrorMessage = Messages._requiredIdCategori)]
        public int CategoriaId { get; set; }
    }
}
