using FluentValidation.Results;
using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, ValidationResult>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;

        public RegistrarCompraCommandHandler(
            ISolicitacaoCompraRepository solicitacaoCompraRepository,
            IProdutoRepository produtoRepository,
            IUnitOfWork uow,
            IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);

            AdcionarItensCompra(solicitacaoCompra, request.Produtos);

            if (ContemError())
                return ValidationResult;

            solicitacaoCompra.RegistrarCompra();

            await RegistraSolicitacaoCompra(solicitacaoCompra);

            if (ContemError())
                return ValidationResult;

            Commit();
            
            PublishEvents(solicitacaoCompra.Events);

            return ValidationResult;
        }

        private async Task RegistraSolicitacaoCompra(SolicitacaoCompraAgg.SolicitacaoCompra solicitacaoCompra) 
        {
            try
            {
                await _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);
                await _solicitacaoCompraRepository.RegistrarItensCompra(solicitacaoCompra.Itens);
            }
            catch (Exception ex) 
            {
                AdicionarErro(string.Format("Problema ao registrar a solicitação de compra: {0}", ex.Message));
                _solicitacaoCompraRepository.ExcluirItensCompra(solicitacaoCompra.Itens);
                _solicitacaoCompraRepository.ExcluirCompra(solicitacaoCompra);
            }            
        }

        private void AdcionarItensCompra(SolicitacaoCompraAgg.SolicitacaoCompra solicitacaoCompra, List<ProdutosCompraDto> produtosCompra)
        {
            foreach (var produtoCompra in produtosCompra)
            {
                var produto = _produtoRepository.Obter(produtoCompra.Id);
                if (produto is null)
                {
                    AdicionarErro(string.Format("Não foi possivel obter o produto {0}", produtoCompra.Id));
                    return;
                }

                solicitacaoCompra.AdicionarItem(produto, produtoCompra.Quantidade);
            }
        }
    }
}
