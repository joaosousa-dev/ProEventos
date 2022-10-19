using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class EventoPersist : GeralPersist, IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersist(ProEventosContext context) : base(context)
        {
            _context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);
            if (includePalestrantes)
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            eventos = eventos.OrderBy(e => e.Id);
            return await eventos.AsNoTracking().ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                            .Include(ev => ev.Lotes)
                            .Include(ev => ev.RedesSociais);
            if (includePalestrantes)
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            eventos = eventos.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await eventos.AsNoTracking().ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                            .Include(ev => ev.Lotes)
                            .Include(ev => ev.RedesSociais);
            if (includePalestrantes)
            {
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }
            eventos = eventos.OrderBy(e => e.Id)
                             .Where(e => e.Id == eventoId);

            return await eventos.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}