using System;
using System.Linq.Expressions;
using TAS.SA.Dominio.Dto;

namespace TAS.SA.Dominio.Filtros
{
    public class ListarDeAnunciosFechados
    {
        public static Expression<Func<Anuncio, bool>> Filtro()
        {
            return x => x.DataFechamento != null;
        }

        public static Expression<Func<Anuncio, ListarDeAnunciosFechadosDTO>> Projecao()
        {
            return x => new ListarDeAnunciosFechadosDTO
            {
                IdAnuncio = x.IdAnuncio,
                NomeProjeto = x.NomeProjeto,
                DataCadastro = x.DataCadastro,
                EstaFechado = x.DataFechamento != null
            };
        }
    }
}
