using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório"),
        MinLength(3, ErrorMessage = "{0} deve ter no mínimo 3 caracteres"),
        MaxLength(50, ErrorMessage = "{0} deve ter no mínimo 3 caracteres")]
        //[StringLength(50,MinimumLength =3,ErrorMessage ="Intervalo entre 3 e 50")]
        public string Tema { get; set; }
        [Display(Name = "Quantidade de pessoas")]
        [Range(1, 50000, ErrorMessage = "{0} precisa ser entre 1 e 50000")]
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        //[RegularExpression(@"[^ \\ s]+(\\.(?i)(jpe?g | png | gif | bmp ))$", ErrorMessage = "{0} Deve ser do tipo bmp,gif,jpeg,jpg ou png")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório"),
        Phone(ErrorMessage = "{0} está inválido")]

        public string Telefone { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório"),
        EmailAddress(ErrorMessage = "{0} Inválido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }

    }
}