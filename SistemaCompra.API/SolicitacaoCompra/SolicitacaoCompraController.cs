using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.API.Base;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using System.Threading.Tasks;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : MainController
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("solicitacoes-compras/registrar")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegistrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            return CustomResponseCreated(await _mediator.Send(registrarCompraCommand));
        }
    }
}
