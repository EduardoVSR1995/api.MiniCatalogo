using api.MiniCatalogo.Config.Const;
using api.MiniCatalogo.DTOs.Request;
using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.MiniCatalogo.Repository.Product
{
    public class OperationProduct
    {
        readonly EntityContext _contextFactory;
        readonly SearchProduct _searchProduct;
        public OperationProduct(IDbContextFactory<EntityContext> contextFactory, SearchProduct searchProduct)
        {
            _contextFactory = contextFactory.CreateDbContext();
            _searchProduct = searchProduct;
        }

        public async Task AddAsync(ProdutoRequestDTO produtoRequestDTO)
        {
            Produto? verifica = await _searchProduct.GetAsync(produtoRequestDTO.Nome);
            
            if (verifica != null && verifica.CategoriaId == produtoRequestDTO.CategoriaId)
                throw new ArgumentException(
                    Messages._nameProductExistInCategori, 
                    nameof(produtoRequestDTO.Nome),
                    new InvalidOperationException(Messages._nameProductExistInCategori)
                );

            Produto produto = new Produto
            {
                Nome = produtoRequestDTO.Nome,
                Preco = produtoRequestDTO.Preco,
                CategoriaId = produtoRequestDTO.CategoriaId
            };

            await _contextFactory.Produtos.AddAsync(produto);

            await _contextFactory.SaveChangesAsync();
        }
    }
}
