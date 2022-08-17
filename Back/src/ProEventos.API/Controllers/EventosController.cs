using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Application.Dtos;
using ProEventos.Persistence;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado!");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null)
                    return NotFound("Nenhum evento encontrado!");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado por tema!");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos por tema. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> IncluiEvento(EventoDto evento)
        {
            try
            {
                var eventoIncluido = await _eventoService.AddEvento(evento);
                if (eventoIncluido == null)
                    return BadRequest("Erro ao tentar salvar evento");

                return Ok(eventoIncluido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao incluir eventos. Erro: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("{eventoId:int}")]
        public async Task<IActionResult> AtualizaEvento(int eventoId, EventoDto evento)
        {
            try
            {
                var eventoAtualizado = await _eventoService.UpdateEvento(eventoId, evento);
                if (eventoAtualizado == null)
                    return NotFound("Evento não encontrado");

                return Ok(eventoAtualizado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar eventos. Erro: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("{eventoId}")]
        public async Task<IActionResult> ExcluiEvento(int eventoId)
        {
            try
            {
                if (!await _eventoService.DeleteEvento(eventoId))
                    return NotFound("Evento não encontrado");
                return Ok($"Evento excluido!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
