namespace api.MiniCatalogo.DTOs.Response
{
    public class ProdutoResponseDTO
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Product name.
        /// </summary>
        public virtual string Nome { get; set; } = null!;
        /// <summary>
        /// Product price.
        /// </summary>
        public virtual decimal Preco { get; set; }
        /// <summary>
        /// Category associated with the product.
        /// </summary>
        public virtual CategoriaResponseDTO Categoria { get; set; } = null!;
    }
}
