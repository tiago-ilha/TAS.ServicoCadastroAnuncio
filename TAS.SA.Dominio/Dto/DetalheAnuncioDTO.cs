using System;
using System.Collections.Generic;
using System.Text;

namespace TAS.SA.Dominio.Dto
{
    public class DetalheAnuncioDTO
    {
        public Guid IdAnuncio { get; set; }
        public string NomeProjeto { get; set; }
        public string DescricaoProjeto { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataFechamento { get; set; }
        public bool EstaFechado => DataFechamento.HasValue;
    }
}
