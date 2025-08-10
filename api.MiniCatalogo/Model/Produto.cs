using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;

namespace api.MiniCatalogo.Model;

public partial class Produto
{
    /// <summary>
    /// Unique identifier of the product.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Product name.
    /// </summary>
    public virtual string Nome { get; set; } = null!;
    /// <summary>
    /// Product price.
    /// </summary>
    public virtual decimal Preco { get; set; }
    /// <summary>
    /// Category identifier of the product.
    /// </summary>
    public virtual int CategoriaId { get; set; }
    /// <summary>
    /// Category associated with the product.
    /// </summary>
    public virtual Categoria Categoria { get; set; } = null!;
}
