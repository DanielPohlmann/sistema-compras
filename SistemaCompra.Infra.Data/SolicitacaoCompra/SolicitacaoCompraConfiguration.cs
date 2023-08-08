using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable(nameof(SolicitacaoCompraAgg.SolicitacaoCompra));
            builder.HasKey(c => c.Id);

            builder.OwnsOne(p => p.TotalGeral, totalBuilder =>
            {
                totalBuilder.Property(m => m.Value).HasColumnName(nameof(SolicitacaoCompraAgg.SolicitacaoCompra.TotalGeral));
            });

            builder.OwnsOne(p => p.CondicaoPagamento, totalBuilder =>
            {
                totalBuilder.Property(m => m.Valor).HasColumnName(nameof(SolicitacaoCompraAgg.SolicitacaoCompra.CondicaoPagamento));
            });

            builder.OwnsOne(p => p.UsuarioSolicitante, usuarioBuilder =>
            {
                usuarioBuilder.Property(m => m.Nome).HasColumnName(nameof(SolicitacaoCompraAgg.SolicitacaoCompra.UsuarioSolicitante));
            });

            builder.OwnsOne(p => p.NomeFornecedor, fornecedorBuilder =>
            {
                fornecedorBuilder.Property(m => m.Nome).HasColumnName(nameof(SolicitacaoCompraAgg.SolicitacaoCompra.NomeFornecedor));
            });
        }
    }    
}
