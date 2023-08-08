using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Domain.Core.Model
{
    public class Money : ValueObject<Money>
    {
        public Money()
                : this(0m)
        {
        }

        public Money(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        public Money Subtract(Money money)
        {
            return new Money(Value - money.Value);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Value };
        }
    }
}
