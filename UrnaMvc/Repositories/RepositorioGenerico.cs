using Microsoft.EntityFrameworkCore;
using UrnaMvc.Data;
using UrnaMvc.Interfaces;

namespace UrnaMvc.Repositories
{
    // Repositório Genérico, é utilizado para itens que há uma repetição muito grande em vários locais, ex CRUD.
    // Porém, deve ser observado se não irá, na verdade, atrapalhar o desempenho do seu código,
    // dependendo da situação, é melhor utilizar diretamento o DbContext e Entity para fazer o Crud, mas depende de cada situação. 
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public RepositorioGenerico(DataContext context)
        {
            _context = context;
        }

        public async Task Alterar(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await RecuperarPorId(id);
                await Excluir(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TEntity>> ListarTodos()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> RecuperarPorId(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
