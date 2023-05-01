
using DomainEntities.Base;

namespace DomainEntities.Example.ValueObjects
{
    public class AverageRating : ValueObject
    {
        public int NumRatings { get; private set; }
        public double Value { get; private set; }
        public AverageRating(int numRatings = 0, double value = 0)
        {
            NumRatings = numRatings;
            Value = value;
        }
        public void AddNewRating(Rating rate)
        {
            Value = (Value * NumRatings + rate.Value) / ++NumRatings;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return NumRatings;
            yield return Value;
        }
    }
}
