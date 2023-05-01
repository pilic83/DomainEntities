using DomainEntities.Base;
using DomainEntities.Helpers;

namespace DomainEntities
{
    public class IDobject<Tobject> : ID<Tobject>
        where Tobject : ValueObject, new()
    {
        public IDobject() : base(RandomClassGenerator<Tobject>.GetRandomObject())
        {
        }
        public static TID CreateWithID<TID>(Tobject value)
            where TID : IDobject<Tobject>, new()
        {
            return Create<TID>(value);
        }
    }
}
