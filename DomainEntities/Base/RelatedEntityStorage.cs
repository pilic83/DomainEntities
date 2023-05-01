using DomainEntities.Base.Interfaces;
using System.Reflection;

namespace DomainEntities.Base
{
    internal class RelatedEntityStorage : IRelatedEntitiesLoader<object>,
                                        IRelatedEntitiesLoader<IID>,
                                        IRelatedEntitiesLoader<IEntity>
    {
        private Dictionary<string, List<object>> RelatedEntitiesID = new();
        private Dictionary<string, List<IID>> RelatedEntitiesStronglyTypedID = new();
        private Dictionary<string, List<IEntity>> RelatedEntities = new();
        private string lastKey = string.Empty;

        public IRelatedEntitiesLoader<IEntity> RelatedEntity(string EntityName)
        {
            lastKey = EntityName;
            if (!RelatedEntities.ContainsKey(lastKey))
            {
                RelatedEntities[lastKey] = new();
            }
            return this;
        }
        public IRelatedEntitiesLoader<IID> RelatedEntityStronglyTypedID(string EntityName)
        {
            lastKey = EntityName;
            if (!RelatedEntitiesStronglyTypedID.ContainsKey(lastKey))
            {
                RelatedEntitiesStronglyTypedID[lastKey] = new();
            }
            return this;
        }
        public IRelatedEntitiesLoader<object> RelatedEntityID(string EntityName)
        {
            lastKey = EntityName;
            if (!RelatedEntitiesID.ContainsKey(lastKey))
            {
                RelatedEntitiesID[lastKey] = new();
            }
            return this;
        }

        public List<T> GetEntities<T>(string EntityName)
            where T : class, IEntity
        {
            return RelatedEntities[EntityName]
                .ConvertAll(entity => entity as T)!;
        }

        public List<T> GetEntityStronglyTypedIDs<T>(string EntityName)
            where T : class, IID
        {
            return RelatedEntitiesStronglyTypedID[EntityName]
                .ConvertAll(stId => stId as T)!;
        }
        private List<T> GetEntityIDsAsStruct<T>(string EntityName)
            where T : struct
        {
            return RelatedEntitiesID[EntityName].ConvertAll(id => (T)id)!;
        }
        private List<T> GetEntityIDsAsValueObject<T>(string EntityName)
            where T : ValueObject
        {
            return RelatedEntitiesID[EntityName].ConvertAll(id => id as T)!;
        }
        private List<string> GetEntityIDsAsString(string EntityName)
        {
            return RelatedEntitiesID[EntityName].ConvertAll(id => id.ToString())!;
        }
        public List<T> GetEntityIDs<T>(string EntityName)
        {
            if (typeof(T) == typeof(int))
            {
                return (dynamic)GetEntityIDsAsStruct<int>(EntityName);
            }
            if (typeof(T) == typeof(decimal))
            {
                return (dynamic)GetEntityIDsAsStruct<decimal>(EntityName);
            }
            if (typeof(T) == typeof(Guid))
            {
                return (dynamic)GetEntityIDsAsStruct<Guid>(EntityName);
            }
            if (typeof(T) == typeof(DateTime))
            {
                return (dynamic)GetEntityIDsAsStruct<DateTime>(EntityName);
            }
            if (typeof(T).BaseType == typeof(ValueObject))
            {
                MethodInfo method = typeof(RelatedEntityStorage).GetMethod("GetEntityIDsAsValueObject", BindingFlags.NonPublic | BindingFlags.Instance)!;
                MethodInfo genericMethod = method.MakeGenericMethod(typeof(T));
                object[] parameters = { EntityName };

                return (dynamic)genericMethod.Invoke(this, parameters)!;
            }
            if (typeof(T) == typeof(string))
            {
                return (dynamic)GetEntityIDsAsString(EntityName);
            }
            return null!;
        }

        public IRelatedEntitiesLoader<object> Add(object entity)
        {
            RelatedEntitiesID[lastKey].Add(entity);
            return this;
        }

        public IRelatedEntitiesLoader<object> AddRange(IEnumerable<object> entities)
        {
            RelatedEntitiesID[lastKey].AddRange(entities);
            return this;
        }

        public IRelatedEntitiesLoader<IID> Add(IID entity)
        {
            RelatedEntitiesStronglyTypedID[lastKey].Add(entity);
            return this;
        }

        public IRelatedEntitiesLoader<IID> AddRange(IEnumerable<IID> entities)
        {
            RelatedEntitiesStronglyTypedID[lastKey].AddRange(entities);
            return this;
        }

        public IRelatedEntitiesLoader<IEntity> Add(IEntity entity)
        {
            RelatedEntities[lastKey].Add(entity);
            return this;
        }

        public IRelatedEntitiesLoader<IEntity> AddRange(IEnumerable<IEntity> entities)
        {
            RelatedEntities[lastKey].AddRange(entities);
            return this;
        }
    }
}
