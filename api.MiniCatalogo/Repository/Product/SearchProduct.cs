using api.MiniCatalogo.DTOs.Request;
using api.MiniCatalogo.DTOs.Response;
using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.MiniCatalogo.Repository.Product
{
    public class SearchProduct
    {
        readonly EntityContext _contextFactory;
        public SearchProduct(IDbContextFactory<EntityContext> contextFactory)
        {
            _contextFactory = contextFactory.CreateDbContext();
        }
        public async Task<List<ProdutoResponseDTO>> GetListPages(SearchListProduct searchListProduct)
         => await _contextFactory.Produtos
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .Skip((searchListProduct.Page - 1) * searchListProduct.Size)
                .Take(searchListProduct.Size)
                .Select(e => new ProdutoResponseDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Preco = e.Preco,
                    Categoria = new CategoriaResponseDTO
                    {
                        Id = e.Categoria.Id,
                        Nome = e.Categoria.Nome
                    },
                }).ToListAsync();
        public async Task<Produto?> GetAsync(string nome)
            => await _contextFactory.Produtos.Where(e => e.Nome.ToLower() == nome.ToLower()).FirstOrDefaultAsync();
    }
}
