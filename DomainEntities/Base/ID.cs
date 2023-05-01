using DomainEntities.Base.Interfaces;

namespace DomainEntities.Base
{
    public class ID<Tvalue> : ValueObject, IID
        where Tvalue : notnull
    {
        public Tvalue Value { get; private set; }
        public ID(Tvalue value)
        {
            Value = value;
        }
        public void SetId(object id)
        {
            Value = (Tvalue)id;
        }
        public object GetId()
        {
            return Value;
        }

        internal static TID Create<TID>(Tvalue value)
            where TID : ID<Tvalue>, new()
        {
            TID tvalue = new TID();
            tvalue.SetId(value);
            return tvalue;
        }    
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
