using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TAS.SA.Dominio.Dto;
using TAS.SA.Dominio.Filtros;

namespace TAS.SA.Dominio
{
    public interface IAnuncioRepositorio
    {
        IEnumerable<ListarAnuncioDTO> ListarAnuncios();
        IEnumerable<ListarDeAnunciosFechadosDTO> ListarAnunciosFechados();
        Anuncio ObterPorId(Guid id);
        void Salvar(Anuncio anuncio);
        void Atualizar(Anuncio anuncio);
    }
}
