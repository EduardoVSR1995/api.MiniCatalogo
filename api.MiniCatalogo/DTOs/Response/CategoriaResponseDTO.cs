namespace api.MiniCatalogo.DTOs.Response
{
    public class CategoriaResponseDTO
    {
        /// <summary>
        /// Unique identifier of the category.
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Category name.
        /// </summary>
        public virtual string Nome { get; set; } = null!;
    }
}
