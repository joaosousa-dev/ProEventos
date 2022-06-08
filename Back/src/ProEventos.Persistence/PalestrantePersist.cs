using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrantes
                            .Include(p => p.RedeSociais);
            if (includeEventos)
                palestrantes = palestrantes.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);

            palestrantes = palestrantes.OrderBy(p => p.Id);
            return await palestrantes.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrantes
                            .Include(p => p.RedeSociais);
            if (includeEventos)
                palestrantes = palestrantes.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);

            palestrantes = palestrantes.OrderBy(e => e.Id).Where(e => e.Nome.ToLower().Contains(nome.ToLower()));
            return await palestrantes.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> palestrante = _context.Palestrantes
                                        .Include(p => p.RedeSociais);
            if (includeEventos)
                palestrante = palestrante.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);

            palestrante = palestrante.OrderBy(p => p.Id)
                                     .Where(p => p.Id == palestranteId);
            return await palestrante.FirstOrDefaultAsync();
        }



    }
}