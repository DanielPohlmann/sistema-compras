using FluentAssertions;
using Microsoft.VisualBasic;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            solicitacao.AdicionarItem(produto, 50);

            //Quando
            solicitacao.RegistrarCompra();

            //Então
            solicitacao.CondicaoPagamento.Valor.Should().Be(SolicitacaoCompra.CONDICAOPAGAMENTO30);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra());

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }
    }
}
