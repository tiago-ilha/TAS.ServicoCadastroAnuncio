using System;
using System.Collections.Generic;
using System.Text;

namespace TAS.SA.Dominio.Dto
{
    public class ListarDeAnunciosFechadosDTO
    {
        public Guid IdAnuncio { get; set; }
        public string NomeProjeto { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool EstaFechado { get; set; }
    }
}
