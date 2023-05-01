using DomainEntities.Base;
using DomainEntities.Example.StronglyTypedIDs;

namespace DomainEntities.Example.Entities
{
    public class MenuItem : Entity<MenuItemId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MenuItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
