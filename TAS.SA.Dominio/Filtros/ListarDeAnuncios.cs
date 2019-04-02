using System;
using System.Linq.Expressions;
using TAS.SA.Dominio.Dto;

namespace TAS.SA.Dominio.Filtros
{
    public class ListarDeAnuncios
    {
        public static Expression<Func<Anuncio, bool>> Filtro()
        {
            return null;
        }

        public static Expression<Func<Anuncio, ListarAnuncioDTO>> Projecao()
        {
            return x => new ListarAnuncioDTO
            {
                IdAnuncio = x.IdAnuncio,
                NomeProjeto = x.NomeProjeto,
                DataCadastro = x.DataCadastro,
                EstaFechado = x.DataFechamento != null
            };
        }
    }
}
