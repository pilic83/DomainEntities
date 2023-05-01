
using DomainEntities.Base;
using DomainEntities.Test.StronglyTypedIDs;

namespace DomainEntities.Test.Entities
{
    public class TestEntity : Entity<PriceAsID>
    {
        public int age { get; set; } = 25;
        public string name { get; set; } = "Mike";
        public DateTime date { get; set; } = DateTime.Now;
    }
}
