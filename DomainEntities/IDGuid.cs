using DomainEntities.Base;

namespace DomainEntities
{
    public class IDGuid : ID<Guid>
    {
        public IDGuid() : base(Guid.NewGuid())
        { }

        public static TID CreateWithID<TID>(Guid value)
            where TID : IDGuid, new()
        {
            return Create<TID>(value);
        }
    }
}
