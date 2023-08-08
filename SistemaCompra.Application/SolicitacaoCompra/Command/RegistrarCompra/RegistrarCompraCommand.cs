using FluentValidation;
using System.Collections.Generic;
using static SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra.ProdutosCompraDto;
using MessagesCommand = SistemaCompra.Domain.Core.Messages;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : MessagesCommand.Command
    {
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public List<ProdutosCompraDto> Produtos { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarCompraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarCompraCommandValidation : AbstractValidator<RegistrarCompraCommand>
        {
            public RegistrarCompraCommandValidation()
            {

                RuleFor(c => c.NomeFornecedor)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Nome do fornecedor é obrigatório!");

                RuleFor(c => c.UsuarioSolicitante)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Usuário Solicitante é obrigatório!");

                RuleFor(c => c.UsuarioSolicitante)
                    .MinimumLength(10)
                    .WithMessage("Nome de fornecedor deve ter pelo menos 10 caracteres.");

                RuleFor(c => c.UsuarioSolicitante)
                    .MinimumLength(5)
                    .WithMessage("Nome de usuário deve possuir pelo menos 8 caracteres.");

                RuleForEach(x => x.Produtos).SetValidator(new ProdutosCompraDtoValidation());
            }
        }
    }
}
