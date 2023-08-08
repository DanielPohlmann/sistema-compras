using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public const decimal MAXTOTALGERAL_CONDICAOPAGAMENTO30 = 50000;
        public const int CONDICAOPAGAMENTO30 = 30;

        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens = new List<Item>();
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra()
        {
            if (Itens is null || !Itens.Any())
            {
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
            }

            var somaSubTotalItens = Itens.Sum(x => x.Subtotal.Value);

            TotalGeral = new Money(somaSubTotalItens);
            CondicaoPagamento = new CondicaoPagamento(0);

            if (TotalGeral.Value > CONDICAOPAGAMENTO30)
            {
                CondicaoPagamento = new CondicaoPagamento(30);
            }


            AddEvent(new CompraRegistradaEvent(Id, Itens, TotalGeral.Value));
        }
    }
}
