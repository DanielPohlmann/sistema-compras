using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class ItensSolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.Item>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.Item> builder)
        {
            builder.ToTable(SolicitacaoCompraAgg.Item.NomeTabela);
            builder.HasKey(e => new { e.ProdutoId, e.SolicitacaoCompraId });
            builder.HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.SolicitacaoCompra)
                .WithMany(s => s.Itens)
                .HasForeignKey(i => i.SolicitacaoCompraId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(i => i.Qtde)
                .IsRequired();
        }
    }
}
