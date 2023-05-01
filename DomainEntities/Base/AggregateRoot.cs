using DomainEntities.Base.Interfaces;
using System.Reflection;

namespace DomainEntities.Base
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IEquatable<AggregateRoot<TId>>
        where TId : notnull, IID, new()
    {
        private readonly RelatedEntityStorage _storage = new();
        protected IRelatedEntitiesLoader<IEntity> RelatedEntity(string EntityName)
        {
            return _storage.RelatedEntity(EntityName);
        }
        protected IRelatedEntitiesLoader<IID> RelatedEntityStronglyTypedID(string EntityName)
        {
            return _storage.RelatedEntityStronglyTypedID(EntityName);
        }
        protected IRelatedEntitiesLoader<object> RelatedEntityID(string EntityName)
        {
            return _storage.RelatedEntityID(EntityName);
        }

        protected List<T> GetEntities<T>(string EntityName)
            where T : class, IEntity
        {
            return _storage.GetEntities<T>(EntityName);
        }

        protected List<T> GetEntityStronglyTypedIDs<T>(string EntityName)
            where T : class, IID
        {
            return _storage.GetEntityStronglyTypedIDs<T>(EntityName);
        }
        protected List<T> GetEntityIDs<T>(string EntityName)
        {
            return _storage.GetEntityIDs<T>(EntityName);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            AggregateRoot<TId>? aggregateRootObj = obj as AggregateRoot<TId>;
            return Equals(aggregateRootObj);
        }
        public bool Equals(AggregateRoot<TId>? other)
        {
            if (other is null)
            {
                return false;
            }
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(AggregateRoot<TId> left, AggregateRoot<TId> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot<TId> left, AggregateRoot<TId> right)
        {
            return !left.Equals(right);
        }
        public static new TaggregateRoot CreateWithID<TaggregateRoot>(object value)
            where TaggregateRoot : AggregateRoot<TId>, new()
        {
            TaggregateRoot tvalue = new TaggregateRoot();
            tvalue.Id.SetId(value);
            return tvalue;
        }

        public static new TaggregateRoot CreateWithStronglyTypedID<TaggregateRoot>(TId id)
            where TaggregateRoot : AggregateRoot<TId>, new()
        {
            TaggregateRoot tvalue = new TaggregateRoot();
            tvalue.Id.SetId(id.GetId());
            return tvalue;
        }

    }
}
