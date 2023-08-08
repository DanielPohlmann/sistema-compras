using FluentValidation.Results;
using MediatR;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.UoW;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Application
{
    public abstract class CommandHandler
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _uow;
        protected ValidationResult ValidationResult;

        public CommandHandler(IUnitOfWork uow, IMediator mediator)
        {
            this._mediator = mediator;
            _uow = uow;
            ValidationResult = new ValidationResult();
        }

        public bool Commit()
        {
            if (_uow.Commit()) return true;

            return false;
        }

        public void PublishEvents(IList<Event> events)
        {
            events.ToList().ForEach(e => _mediator.Publish(e));
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected bool ContemError() => ValidationResult.Errors.Any();
    }
}
