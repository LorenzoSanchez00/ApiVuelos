using ApiVuelos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiVuelos.Repository
{
    public class AerolineaRepository : IRepository<Aerolinea>
    {
        private readonly VuelosDbContext _context;

        public AerolineaRepository(VuelosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aerolinea>> Get() => 
            await _context.Aerolineas.ToListAsync();

        public async Task<Aerolinea> GetById(int id) =>
            await _context.Aerolineas.FindAsync(id);

        public  async Task Add(Aerolinea aerolinea) => 
            await _context.Aerolineas.AddAsync(aerolinea);

        public void Update(Aerolinea aerolinea)
        {
            _context.Aerolineas.Attach(aerolinea);
            _context.Aerolineas.Entry(aerolinea).State = EntityState.Modified;
        }
        public void Delete(Aerolinea aerolinea)
        {
            _context.Aerolineas.Remove(aerolinea);
        }


        public Task Save()
        {
            throw new NotImplementedException();
        }

    }
}
