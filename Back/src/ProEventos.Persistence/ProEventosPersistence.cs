using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {
        private readonly ProEventosContext _context;

        public ProEventosPersistence(ProEventosContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedeSociais);
            if (includePalestrantes)
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            eventos = eventos.OrderBy(e => e.Id);
            return await eventos.ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                            .Include(ev => ev.Lotes)
                            .Include(ev => ev.RedeSociais);
            if (includePalestrantes)
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            eventos = eventos.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await eventos.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                            .Include(ev => ev.Lotes)
                            .Include(ev => ev.RedeSociais);
            if (includePalestrantes)
            {
                eventos = eventos.Include(ev => ev.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }
            eventos = eventos.OrderBy(e => e.Id)
                             .Where(e => e.Id == EventoId);

            return await eventos.FirstOrDefaultAsync();
        }

        public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            throw new System.NotImplementedException();
        }

        public Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            throw new System.NotImplementedException();
        }


        public Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos)
        {
            throw new System.NotImplementedException();
        }



    }
}