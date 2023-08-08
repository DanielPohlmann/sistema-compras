using FluentValidation.Results;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace SistemaCompra.Domain.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
