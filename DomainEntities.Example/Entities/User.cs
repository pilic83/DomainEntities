using DomainEntities.Base;
using DomainEntities.Example.StronglyTypedIDs;

namespace DomainEntities.Example.Entities
{
    public class User : Entity<UserId>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
