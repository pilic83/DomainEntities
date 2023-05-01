using DomainEntities.Base;
using DomainEntities.Example.Entities;
using DomainEntities.Example.StronglyTypedIDs;

namespace DomainEntities.Example.AggregateRoots
{
    public class MenuSection : AggregateRoot<MenuSectionId>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public IReadOnlyList<MenuItem> Items => GetEntities<MenuItem>(nameof(MenuItem)).AsReadOnly();
        public MenuSection(List<MenuItem> items, string name, string description) 
        {
            RelatedEntity(nameof(MenuItem))
                .AddRange(items);
            Name = name;
            Description = description;
        }
    }
}
