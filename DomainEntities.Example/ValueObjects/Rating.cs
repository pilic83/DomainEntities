using DomainEntities.Base;

namespace DomainEntities.Example.ValueObjects
{
    public class Rating : ValueObject
    {
        public int Value { get; init; }
        public Rating(int value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
