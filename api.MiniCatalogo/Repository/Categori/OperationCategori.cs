using api.MiniCatalogo.Config.Const;
using api.MiniCatalogo.DTOs.Request;
using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.MiniCatalogo.Repository.Categori
{
    public class OperationCategori
    {
        readonly EntityContext _contextFactory;
        readonly SearchCategori _searchCategori;
        public OperationCategori(IDbContextFactory<EntityContext> contextFactory, SearchCategori searchCategori)
        {
            _contextFactory = contextFactory.CreateDbContext();
            _searchCategori = searchCategori;
        }

        public async Task AddAsync(CategoriaRequestDTO categoriaRequestDTO)
        {
            Categoria? verifica = await _searchCategori.GetAsync(categoriaRequestDTO.Nome);
            
            if (verifica != null)
                throw new ArgumentException(
                    Messages._categoriExist, 
                    nameof(categoriaRequestDTO.Nome),
                    new InvalidOperationException(Messages._categoriExist)
                );

            Categoria categoria = new Categoria
            {
                Nome = categoriaRequestDTO.Nome
            };

            await _contextFactory.Categoria.AddAsync(categoria);

            await _contextFactory.SaveChangesAsync();
        }
    }
}
