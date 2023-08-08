using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class Item
    {
        public const string NomeTabela = "ItensSolicitacaoCompra";

        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public Guid SolicitacaoCompraId { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }

        public int Qtde { get; set; }

        public Money Subtotal => ObterSubtotal();

        public Item(Produto produto, int qtde)
        {
            Produto = produto ?? throw new ArgumentNullException(nameof(produto));
            Qtde = qtde;
        }

        private Money ObterSubtotal()
        {
            return new Money(Produto.Preco.Value * Qtde);
        }

        private Item() { }

    }
}
