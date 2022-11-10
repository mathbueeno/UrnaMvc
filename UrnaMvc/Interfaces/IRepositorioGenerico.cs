namespace UrnaMvc.Interfaces
{
    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        Task<List<TEntity>> ListarTodos();
        Task<TEntity> RecuperarPorId(int id);
        Task Inserir(TEntity entity);
        Task Alterar(TEntity entity);
        Task Excluir(int id);
        Task Excluir(TEntity entity);
    }
}
