
namespace DomainEntities.Base
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private const int primeHash = 41;
        public abstract IEnumerable<object> GetEqualityComponents();
        public bool Equals(ValueObject? other)
        {
            if (other is null)
            {
                return false;
            }
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
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
            ValueObject? valueObj = obj as ValueObject;
            return Equals(valueObj);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            GetEqualityComponents().ToList().ForEach(component => hash += primeHash * component.GetHashCode());
            return hash;
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !left.Equals(right);
        }
    }
}