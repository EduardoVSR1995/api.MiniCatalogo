using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Test.MiniCatalogo.Seed
{
    public class PopulateDb
    {
        readonly IDbContextFactory<EntityContext> _factori;
        
        public PopulateDb(IDbContextFactory<EntityContext> factori) 
        {
            _factori = factori;
        }
        public async Task Create_Categori(Categoria categoria)
        {
            var ctx = await _factori.CreateDbContextAsync();
            await ctx.Categoria.AddAsync(categoria);
            await ctx.SaveChangesAsync();
        }
        public async Task Create_Product(Produto produto)
        {
            var ctx = _factori.CreateDbContext();
            await ctx.Produtos.AddAsync(produto);
            await ctx.SaveChangesAsync();
        }
        public void Dispose()
        {
            var context = _factori.CreateDbContext();
            context.Produtos.ExecuteDelete();
            context.Categoria.ExecuteDelete();
            context.SaveChanges();
        }
    }
}
