namespace ApiVuelos.Services
{
    public interface IVueloService<T>
    {
        Task<IEnumerable<T>> GetByPriceAsc();
        Task<IEnumerable<T>> GetByPriceDesc();
    }
}
