
using DomainEntities.Base;
using DomainEntities.Test.StronglyTypedIDs;

namespace DomainEntities.Test.Entities
{
    public class FloatedEntity : Entity<FloatedId>
    {
        public string title { get; set; } = "floated";
    }
}
