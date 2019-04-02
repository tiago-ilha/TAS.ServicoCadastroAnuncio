using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using TAS.SA.Api.ViewModels;
using TAS.SA.Dominio;
using TAS.SA.Dominio.Dto;

namespace TAS.SA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepositorio _repositorio;

        public AnuncioController(IAnuncioRepositorio repositorio) => _repositorio = repositorio;

        // GET: api/Anuncio
        [HttpGet]
        public IActionResult ListarAnuncios() => RetornoListagem(_repositorio.ListarAnuncios());

        [HttpGet("fechados")]
        public IActionResult ListarAnunciosAtrasados() => RetornoListagem(_repositorio.ListarAnunciosFechados());

        // GET: api/Anuncio/5
        [HttpGet("{idAnuncio}")]
        public IActionResult DetalhesAnuncio(Guid idAnuncio)
        {
            if (idAnuncio == Guid.Empty)
                return BadRequest();

            var registro = RecuperarAnuncioPorId(idAnuncio);

            if (registro == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado.", null));

            return Ok(new DetalheAnuncioDTO {
                IdAnuncio = registro.IdAnuncio,
                NomeProjeto = registro.NomeProjeto,
                DescricaoProjeto = registro.DescricaoProjeto,
                DataCadastro = registro.DataCadastro,
                DataFechamento = registro.DataFechamento
            });
        }

        // POST: api/Anuncio
        [HttpPost]
        public IActionResult Cadastrar([FromBody] RegistrarAnuncioViewModel viewModel)
        {
            if (viewModel.Invalid)
                return BadRequest(RetornoAcao(viewModel.Valid, "Não foi possível completar operação.", viewModel.Notifications));

            var anuncio = new Anuncio(viewModel.NomeProjeto, viewModel.DescricaoProjeto);

            _repositorio.Salvar(anuncio);

            var url = Url.Action("DetalhesAnuncio", new { idAnuncio = anuncio.IdAnuncio });

            return Created(url, RetornoAcao(viewModel.Valid, "Operação foi realizada com sucesso."));
        }

        // PUT: api/Anuncio/5
        [HttpPut("{idAnuncio}")]
        public IActionResult Alterar(Guid idAnuncio, [FromBody] RegistrarAnuncioViewModel viewModel)
        {
            if (viewModel.Invalid)
                return BadRequest(RetornoAcao(viewModel.Valid, "Não foi possível completar operação.", viewModel.Notifications));

            var anuncio = _repositorio.ObterPorId(idAnuncio);

            if (anuncio == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado."));

            anuncio.AlterarNome(viewModel.NomeProjeto);
            anuncio.AlterarDescricao(viewModel.DescricaoProjeto);

            _repositorio.Atualizar(anuncio);

            return Ok(RetornoAcao(viewModel.Valid, "Operação foi realizada com sucesso."));
        }

        [HttpPut("{idAnuncio}/fechar")]
        public IActionResult FecharAnuncio(Guid idAnuncio)
        {
            if (idAnuncio == Guid.Empty)
                return BadRequest();

            var anuncio = RecuperarAnuncioPorId(idAnuncio);

            if (anuncio == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado."));

            anuncio.Finalizar();

            if (anuncio.Invalid)
                return BadRequest(RetornoAcao(anuncio.Valid, "Não foi possível completar operação.", anuncio.Notifications));

            _repositorio.Atualizar(anuncio);

            return Ok(RetornoAcao(anuncio.Valid, "Operação foi realizada com sucesso."));
        }

        #region Métodos compartilhados

        private IActionResult RetornoListagem(IEnumerable<dynamic> anuncios)
        {
            if (PossuiAnuncios(anuncios))
                return NoContent();

            return Ok(anuncios);
        }

        private bool PossuiAnuncios(IEnumerable<dynamic> anuncios) => anuncios == null || anuncios.Count() == 0;

        private Anuncio RecuperarAnuncioPorId(Guid idAnuncio)
        {
            return _repositorio.ObterPorId(idAnuncio);
        }

        private object RetornoAcao(bool estaValido, string mensagem, IReadOnlyCollection<Notification> erros = null)
        {
            return new
            {
                EstaValid = estaValido,
                Mensagem = mensagem,
                Erros = erros != null && erros.Count > 0 ? erros : new List<Notification>()
            };
        }

        #endregion
    }
}
