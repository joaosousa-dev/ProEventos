using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;

        public LoteService(ILotePersist lotePersist, IMapper mapper)
        {
            _lotePersist = lotePersist;
            _mapper = mapper;
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                _lotePersist.Add<Lote>(lote);
                await _lotePersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] lotesDto)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                foreach (var model in lotesDto)
                {
                    if (model.Id == 0)
                    {
                        model.EventoId = eventoId;
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(x => x.Id == model.Id);
                        model.EventoId = eventoId;
                        _mapper.Map(model, lote);
                        _lotePersist.Update<Lote>(lote);
                        await _lotePersist.SaveChangesAsync();
                    }
                }
                return _mapper.Map<LoteDto[]>(await _lotePersist.GetLotesByEventoIdAsync(eventoId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);

                if (lote != null)
                {
                    _lotePersist.Delete<Lote>(lote);
                    return await _lotePersist.SaveChangesAsync();
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;
                var resultado = _mapper.Map<LoteDto[]>(lotes);
                return resultado;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);
                return resultado;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}