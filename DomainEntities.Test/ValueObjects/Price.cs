using DomainEntities.Base;

namespace DomainEntities.Test.ValueObjects
{
    public class Price : ValueObject
    {
        public float Value { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public Price() { }
        public Price(float value, string currency)
        {
            Value = value;
            Currency = currency;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
