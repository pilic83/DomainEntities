using DomainEntities.Base;

namespace DomainEntities
{
    public class IDDateTime : ID<DateTime>
    {
        public IDDateTime() : base(DateTime.Now)
        {   }
        public static TID CreateWithID<TID>(DateTime value)
            where TID : IDDateTime, new()
        {
            return Create<TID>(value);
        }
    }
}
