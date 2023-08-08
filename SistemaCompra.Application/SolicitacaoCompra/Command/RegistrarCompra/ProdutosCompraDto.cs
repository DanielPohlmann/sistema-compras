using FluentValidation;
using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ProdutosCompraDto
    {
        public Guid Id { get; set; }

        public int Quantidade { get; set; }

        public class ProdutosCompraDtoValidation : AbstractValidator<ProdutosCompraDto>
        {
            public ProdutosCompraDtoValidation()
            {
                RuleFor(c => c.Id)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Item não deve ser null ou vazio!");

                RuleFor(c => c.Quantidade)
                   .NotNull()
                   .NotEmpty()
                   .WithMessage("Quantidade deve ser maior que 0");
            }
        }
    }
}
