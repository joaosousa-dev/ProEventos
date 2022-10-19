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
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;
        public LotesController(ILoteService loteService)
        {
            _loteService = loteService;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var eventos = await _eventoService.GetEventoByIdAsync(true);
                if (eventos == null)
                    return NoContent();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }




        [HttpPut]
        [Route("{eventoId:int}")]
        public async Task<IActionResult> AtualizaEvento(int eventoId, LoteDto[] models)
        {
            try
            {
                var eventoAtualizado = await _eventoService.UpdateEvento(eventoId, models);
                if (eventoAtualizado == null)
                    return NoContent();

                return Ok(eventoAtualizado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar eventos. Erro: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("{eventoId}/{loteId}")]
        public async Task<IActionResult> ExcluiEvento(int eventoId, int loteId)
        {
            try
            {
                if (!await _eventoService.DeleteEvento(eventoId))
                    return NoContent();
                return Ok(new { message = $"Deletado" });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
