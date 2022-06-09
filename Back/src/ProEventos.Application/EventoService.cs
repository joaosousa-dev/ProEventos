using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventosPersist _eventoPersist;

        public EventoService(IEventosPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _eventoPersist.Add<Evento>(model);
                if (await _eventoPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = _eventoPersist.GetEventoByIdAsync(eventoId);

                if (evento == null)
                    return null;

                model.Id = evento.Id;

                _eventoPersist.Update<Evento>(model);
                if (await _eventoPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);

                if (evento == null)
                    throw new Exception($"Nenhum evento com id : {eventoId}");

                _eventoPersist.Delete<Evento>(evento);
                return await _eventoPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
        }

        public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
        }

        public Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
        }
    }
}