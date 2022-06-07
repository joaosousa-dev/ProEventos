using System;
using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public IEnumerable<Evento> Eventos { get; set; }
        public int? PalestranteId { get; set; }
        public IEnumerable<Palestrante> Palestrantes { get; set; }

    }
}