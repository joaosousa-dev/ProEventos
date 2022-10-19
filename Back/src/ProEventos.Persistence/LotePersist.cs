using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class LotePersist : GeralPersist, ILotePersist
    {
        private readonly ProEventosContext _context;

        public LotePersist(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking().Where(lote => lote.EventoId == eventoId);
            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int Id)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking().Where(lote => lote.EventoId == eventoId && lote.Id == Id);
            return await query.FirstOrDefaultAsync();
        }
    }
}