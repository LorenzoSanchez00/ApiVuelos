namespace ApiVuelos.Repository
{
    public interface IVueloRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetByPriceAsc();
        Task<IEnumerable<TEntity>> GetByPriceDesc();
    }
}
