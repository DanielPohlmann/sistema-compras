using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoAgg.Produto>
    {
        public void Configure(EntityTypeBuilder<ProdutoAgg.Produto> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable(nameof(ProdutoAgg.Produto));
            builder.OwnsOne(x => x.Preco).Property(x => x.Value).HasColumnName(nameof(ProdutoAgg.Produto.Preco));
        }
    }
}
