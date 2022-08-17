using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventosPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(IEventosPersist eventoPersist, IMapper mapper)
        {
            _eventoPersist = eventoPersist;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _eventoPersist.Add<Evento>(evento);
                if (await _eventoPersist.SaveChangesAsync())
                {
                    return _mapper.Map<EventoDto>(await _eventoPersist.GetEventoByIdAsync(evento.Id));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            // try
            // {
            //     var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);

            //     if (evento != null)
            //     {
            //         model.Id = evento.Id;
            //         _eventoPersist.Update<Evento>(model);
            //         if (await _eventoPersist.SaveChangesAsync())
            //         {
            //             return await _eventoPersist.GetEventoByIdAsync(model.Id);
            //         }
            //     }
            //     return null;
            // }
            // catch (Exception ex)
            // {
            //     throw new Exception(ex.Message);
            // } 
            return null;
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);

                if (evento != null)
                {
                    _eventoPersist.Delete<Evento>(evento);
                    return await _eventoPersist.SaveChangesAsync();
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                var resultado = _mapper.Map<EventoDto[]>(eventos);
                return resultado;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                var resultado = _mapper.Map<EventoDto[]>(eventos);
                return resultado;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);
                return resultado;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}