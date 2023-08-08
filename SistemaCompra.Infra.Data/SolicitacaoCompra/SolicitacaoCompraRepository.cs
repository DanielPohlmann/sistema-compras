using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SolicitacaoCompraAgg.ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            this.context = context;
        }

        public async Task RegistrarCompra(SolicitacaoCompraAgg.SolicitacaoCompra solicitacaoCompra)
        {
            await context.Set<SolicitacaoCompraAgg.SolicitacaoCompra>().AddAsync(solicitacaoCompra);
        }

        public async Task RegistrarItensCompra(IEnumerable<Item> itensSolicitacaoCompra)
        {
            await context.Set<Item>().AddRangeAsync(itensSolicitacaoCompra);
        }

        public void ExcluirCompra(SolicitacaoCompraAgg.SolicitacaoCompra entity)
        {
            context.Set<SolicitacaoCompraAgg.SolicitacaoCompra>().Remove(entity);
        }

        public void ExcluirItensCompra(IEnumerable<Item> entitys)
        {
            context.Set<Item>().RemoveRange(entitys);
        }       
    }
}
