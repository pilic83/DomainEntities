using DomainEntities.Base;
using DomainEntities.Test.StronglyTypedIDs;
using DomainEntities.Test.ValueObjects;

namespace DomainEntities.Test.Entities
{
    public class BillEntity : Entity<BillId>
    {
        public Price Price { get; set; } = null!;
    }
}
