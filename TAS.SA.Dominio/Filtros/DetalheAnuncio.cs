using System;
using System.Linq.Expressions;
using TAS.SA.Dominio.Dto;

namespace TAS.SA.Dominio.Filtros
{
    public class DetalheAnuncio
    {
        public static Expression<Func<Anuncio, bool>> Filtro(Guid idAnuncio)
        {
            return x => x.IdAnuncio == idAnuncio;
        }

        public static Expression<Func<Anuncio, DetalheAnuncioDTO>> Projecao()
        {
            return x => new DetalheAnuncioDTO
            {
                IdAnuncio = x.IdAnuncio,
                NomeProjeto = x.NomeProjeto,
                DescricaoProjeto = x.DescricaoProjeto,
                DataCadastro = x.DataCadastro,
                DataFechamento = x.DataFechamento
            };
        }
    }
}
