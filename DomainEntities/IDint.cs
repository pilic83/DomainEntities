using DomainEntities.Base;

namespace DomainEntities
{
    public class IDint : ID<int>
    {
        public IDint() : base(Random.Shared.Next())
        {   }
        public static TID CreateWithID<TID>(int value)
            where TID : IDint, new()
        {
            return Create<TID>(value);
        }
    }
}
