using DomainEntities.Base;
using System.IO;

namespace DomainEntities
{
    public class IDstring : ID<string>
    {
        public IDstring() : base(Path.GetRandomFileName().Replace(".", ""))
        {   }
        public static TID CreateWithID<TID>(string value)
            where TID : IDstring, new()
        {
            return Create<TID>(value);
        }
    }
}
