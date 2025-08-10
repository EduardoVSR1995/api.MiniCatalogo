using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace api.MiniCatalogo.Model;

public partial class Categoria
{
    /// <summary>
    /// Unique identifier of the category.
    /// </summary>
    public virtual int Id { get; set; }
    /// <summary>
    /// Category name.
    /// </summary>
    public virtual string Nome { get; set; } = null!;
    /// <summary>
    /// List of products associated with this category.
    /// </summary>
    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
