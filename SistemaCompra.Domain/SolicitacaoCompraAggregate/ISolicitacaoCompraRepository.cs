using System.Collections.Generic;
using System.Threading.Tasks;
namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface ISolicitacaoCompraRepository
    {
        Task RegistrarCompra(SolicitacaoCompra solicitacaoCompra);
        Task RegistrarItensCompra(IEnumerable<Item> itensSolicitacaoCompra);
        void ExcluirCompra(SolicitacaoCompra entity);
        void ExcluirItensCompra(IEnumerable<Item> entitys);
    }
}
