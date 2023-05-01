using DomainEntities.Base.Interfaces;

namespace DomainEntities.Base
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity
        where TId : notnull, IID, new()
    {
        public TId Id { get; init; }
        public Entity() 
        {
            Id = new TId();
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
            Entity<TId>? entityObj = obj as Entity<TId>;
            return Equals(entityObj);
        }

        public bool Equals(Entity<TId>? other)
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

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !left.Equals(right);
        }
        public static Tentity CreateWithID<Tentity>(object value)
            where Tentity : Entity<TId>, new()
        {
            Tentity tvalue = new Tentity();
            tvalue.Id.SetId(value);
            return tvalue;
        }

        public static Tentity CreateWithStronglyTypedID<Tentity>(TId id)
            where Tentity : Entity<TId>, new()
        {
            Tentity tvalue = new Tentity();
            tvalue.Id.SetId(id.GetId());
            return tvalue;
        }
    }
}
