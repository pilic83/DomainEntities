using DomainEntities.Base;
using DomainEntities.Example.StronglyTypedIDs;
using DomainEntities.Example.ValueObjects;

namespace DomainEntities.Example.AggregateRoots
{
    public class Menu : AggregateRoot<MenuId>
    {
        private AverageRating averageRaiting = new();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AverageRating AverageRaiting
        {
            get => averageRaiting;
            private set
            {
                averageRaiting = new AverageRating(value.NumRatings, value.Value);
            }
        }
        public IReadOnlyList<MenuSection> Sections => GetEntities<MenuSection>(nameof(MenuSection)).AsReadOnly();
        public HostId HostId { get; private set; }
        public IReadOnlyList<DinnerId> DinnerIds => GetEntityStronglyTypedIDs<DinnerId>(nameof(DinnerId)).AsReadOnly();
        public IReadOnlyList<Guid> MenuReviewIds => GetEntityIDs<Guid>(nameof(MenuReviewId)).AsReadOnly();

        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }

        public Menu(List<MenuSection> sections,
            string name,
            string description,
            HostId hostId)
        {
            RelatedEntity(nameof(MenuSection))
                .AddRange(sections);
            Name = name;
            Description = description;
            HostId = hostId;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            RelatedEntityStronglyTypedID(nameof(DinnerId));
            RelatedEntityID(nameof(MenuReviewId));
        }
    }
}
