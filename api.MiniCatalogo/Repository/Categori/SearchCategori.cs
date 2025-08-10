using api.MiniCatalogo.DTOs.Response;
using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.MiniCatalogo.Repository.Categori
{
    public class SearchCategori
    {
        readonly EntityContext _contextFactory;
        public SearchCategori(IDbContextFactory<EntityContext> contextFactory)
        {
            _contextFactory = contextFactory.CreateDbContext();
        }
        public async Task<List<CategoriaResponseDTO>> GetAllAsync()
            => await _contextFactory.Categoria.Select(e => new CategoriaResponseDTO
            {
                Id = e.Id,
                Nome = e.Nome
            }).ToListAsync();

        public async Task<Categoria?> GetAsync(string nome)
            => await _contextFactory.Categoria.Where(e => e.Nome.ToLower() == nome.ToLower()).FirstOrDefaultAsync();
    }
}
