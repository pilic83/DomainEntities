using DomainEntities.Base;
using DomainEntities.Test.Entities;
using DomainEntities.Test.StronglyTypedIDs;
using DomainEntities.Test.ValueObjects;

namespace DomainEntities.Test.AggregateRoots
{
    public class DailySales : AggregateRoot<DataId>
    {
        public int Total { get; set; } = 0;
        public List<BillId> BillIds => GetEntityStronglyTypedIDs<BillId>(nameof(BillEntity));
        public List<TestEntity> Tests => GetEntities<TestEntity>(nameof(TestEntity));

        public List<Price> Floated => GetEntityIDs<Price>(nameof(FloatedEntity));
        public DailySales()
        {
            RelatedEntityStronglyTypedID(nameof(BillEntity))
                .Add((new BillEntity()).Id)
                .Add((new BillEntity()).Id)
                .Add((new BillEntity()).Id);

            RelatedEntity(nameof(TestEntity))
                .Add(new TestEntity())
                .Add(new TestEntity())
                .Add(new TestEntity())
                .Add(new TestEntity())
                .Add(new TestEntity())
                .Add(new TestEntity());

            RelatedEntityID(nameof(FloatedEntity))
                .Add((new FloatedEntity()).Id.Value)
                .Add((new FloatedEntity()).Id.Value)
                .Add((new FloatedEntity()).Id.Value)
                .Add((new FloatedEntity()).Id.Value)
                .Add((new FloatedEntity()).Id.Value);
        }
    }
}
