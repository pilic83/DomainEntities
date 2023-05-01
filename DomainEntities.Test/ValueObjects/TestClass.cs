using DomainEntities.Base;

namespace DomainEntities.Test.ValueObjects
{
    public class TestClass : ValueObject
    {
        public Random randValue { get; set; } = new Random();
        public int intValue { get; set; } = 10;
        public float floatValue { get; set; } = 10;
        public string stringValue { get; set; } = string.Empty;
        public long longValue { get; set; } = 10;
        public char charValue { get; set; } = 'A';
        public double doubleValue { get; set; } = 10;
        public decimal decimalValue { get; set; } = 10;
        public short shortValue { get; set; } = 10;
        public bool boolValue { get; set; } = true;
        public TestEnum enumValue { get; set; } = TestEnum.one;
        public TestEnumValue enumValueValue { get; set; } = TestEnumValue.one;
        public TestStruct structValue { get; set; } = new TestStruct(3, 3);
        public Price priceValue { get; set; } = new Price(10, "RSD");
        public ushort[] ushorts { get; set; } = new ushort[3] { 1, 2, 3 };
        public bool[] bools { get; set; } = new bool[3] { true, true, true };
        public Price[] prices { get; set; } = new Price[2] { new Price(2, "Eur"), new Price(3, "USD") };
        public List<string> strings { get; set; } = new() { "one", "two", "tree" };
        public Dictionary<int, int> dictionary { get; set; } = new() { { 1, 11 }, { 2, 22 } };
        public Tuple<int, string> tuple { get; set; } = Tuple.Create(1, "one");
        public TestRecord recordValue { get; set; } = new TestRecord(1, "two", 3);

        private int intValuePrivate = 20;
        private float floatValuePrivate = 20;
        private string stringValuePrivate = "test";
        private string message = string.Empty;

        public TestClass() { }
        public TestClass(int intValue, float floatValue, string stringValue, long longValue, char charValue, double doubleValue, decimal decimalValue, short shortValue, bool boolValue, int intValuePrivate, float floatValuePrivate, string stringValuePrivate)
        {
            this.intValue = intValue;
            this.floatValue = floatValue;
            this.stringValue = stringValue;
            this.longValue = longValue;
            this.charValue = charValue;
            this.doubleValue = doubleValue;
            this.decimalValue = decimalValue;
            this.shortValue = shortValue;
            this.boolValue = boolValue;
            this.intValuePrivate = intValuePrivate;
            this.floatValuePrivate = floatValuePrivate;
            this.stringValuePrivate = stringValuePrivate;

            message = $"{intValuePrivate} {floatValuePrivate} {stringValuePrivate}";
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return randValue;
            yield return intValue;
            yield return floatValue;
            yield return stringValue;
            yield return longValue;
            yield return charValue;
            yield return doubleValue;
            yield return decimalValue;
            yield return shortValue;
            yield return boolValue;
            yield return enumValue;
            yield return enumValueValue;
            yield return stringValue;
            yield return priceValue;
            yield return ushorts;
            yield return bools;
            yield return prices;
            yield return strings;
            yield return dictionary;
            yield return tuple;
            yield return recordValue;

            yield return intValuePrivate;
            yield return floatValuePrivate;
            yield return stringValuePrivate;
            yield return message;
        }
    }

    public enum TestEnum
    {
        one, two, three, four, five
    }
    public enum TestEnumValue
    {
        one = 3,
        two = 11,
        three = 29,
        four = 41,
        five = 53
    }
    public struct TestStruct
    {
        public int a = 2;
        private float b = 2;
        public TestStruct(int _a, float _b)
        {
            a = _a; b = _b;
        }
    }
    public record TestRecord(int a, string b, float c);
}
