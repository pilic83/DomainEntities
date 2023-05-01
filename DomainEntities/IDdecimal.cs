using DomainEntities.Base;
using System;

namespace DomainEntities
{
    public class IDdecimal : ID<decimal>
    {
        public IDdecimal() : base((decimal)Random.Shared.NextDouble() * (decimal.MaxValue / (decimal)(1<<30)))
        {
        }
        public static TID CreateWithID<TID>(decimal value)
            where TID : IDdecimal, new()
        {
            return Create<TID>(value);
        }
    }
}
