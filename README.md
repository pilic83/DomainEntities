# DomainEntities
A library that provides a robust foundation for working with domain entities in your application, allowing you to quickly and easily model and manipulate your business data.<br/>
A ValueObject class provides support for working with value objects in your application. 
Value objects are objects that represent a conceptually atomic piece of data, such as an email address, a phone number, or a date range. 
They are immutable and their equality is determined by the equality of their attributes. <br/>
```
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
```
```
Price p = new Price(10, "RSD");
var q = new Price(10, "RSD");
Console.WriteLine(p.Equals(q));
```
Our library provides a set of classes to support working with strongly typed IDs in your application. 
These IDs can be of different types, such as Guid, int, decimal, string, DateTime, and even custom ValueObject classes. 
For each ID type, we provide a corresponding ID class, such as IDGuid, IDint, IDdecimal, IDstring, IDDateTime, and IDobject, which is a generic class with a type parameter that can be set to any ValueObject class.<br/>
```
public class BillId : IDGuid
    { }
```
```
//Create strongly typed id with new Guid value
BillId b = new();
Console.WriteLine(b.Value); //Guid id

//Create strongly typed id with specified Guid value
var guid = Guid.NewGuid();
var bill = BillId.CreateWithID<BillId>(guid);
Console.WriteLine(bill.Equals(b));
```
You can even use ValueObject objects for the ID, for example Price objects, but be aware that automatically generating such objects uses reflection, which can reduce application performance.<br/>
```
public class PriceAsID : IDobject<Price>
    {
    }
```
```
PriceAsID objn = new();
var obj = PriceAsID.CreateWithID<PriceAsID>(p);
var obj1 = PriceAsID.CreateWithID<PriceAsID>(new Price(10, "RSD"));
Console.WriteLine(JsonSerializer.Serialize<Price>(obj.Value));
Console.WriteLine(obj1.Equals(obj));
```
Our library also provides support for working with entities through the Entity class, which is a generic class with a type parameter that represents the strongly typed ID that identifies the entity. 
Two entities are considered equal if they have the same ID. This allows you to work with entities in a strongly typed and consistent way, ensuring that they are always compared and identified correctly throughout your application.<br/>
```
public class UserId : IDGuid
    {
    }
```
```
public class User : Entity<UserId>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
```
```
//Create entity user with strongly typed id with new Guid value
User user = new();
var guid = Guid.NewGuid();
var guidID = UserId.CreateWithID<UserId>(guid);
//Create entity with specified strongly typed - UserId id value 
var u1 = User.CreateWithStronglyTypedID<User>(guidID);
//Create entity with specified strongly typed id with Guid value 
var u2 = User.CreateWithID<User>(guid);
Console.WriteLine(u1.Equals(u2));
```
Finally, our library also provides support for working with aggregate roots through the AggregateRoot class. 
Aggregate roots are entities that contain lists of other entities or lists of IDs of other entities, and can have either simple or strongly typed IDs. 
By encapsulating these lists in the AggregateRoot class, you can ensure that they are always managed consistently and correctly throughout your application, providing a clear and concise way to work with complex entity relationships.
The AggregateRoot class is also a generic class with a type parameter that represents the strongly typed ID used to identify the aggregate root. Like entities, two aggregate roots are considered equal if they have the same ID. 
By providing a consistent and strongly typed way to manage aggregate roots, our library makes it easy to work with complex entity relationships and ensure the integrity of your data model.<br/>
```
public class Menu : AggregateRoot<MenuId>
    {
        private AverageRating averageRaiting = new();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AverageRating AverageRaiting
        {
            get => averageRaiting;
            private set
            {
                averageRaiting = new AverageRating(value.NumRatings, value.Value);
            }
        }
        public IReadOnlyList<MenuSection> Sections => GetEntities<MenuSection>(nameof(MenuSection)).AsReadOnly();
        public HostId HostId { get; private set; }
        public IReadOnlyList<DinnerId> DinnerIds => GetEntityStronglyTypedIDs<DinnerId>(nameof(DinnerId)).AsReadOnly();
        public IReadOnlyList<Guid> MenuReviewIds => GetEntityIDs<Guid>(nameof(MenuReviewId)).AsReadOnly();

        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }

        public Menu(List<MenuSection> sections,
            string name,
            string description,
            HostId hostId)
        {
            RelatedEntity(nameof(MenuSection))
                .AddRange(sections);
            Name = name;
            Description = description;
            HostId = hostId;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            RelatedEntityStronglyTypedID(nameof(DinnerId));
            RelatedEntityID(nameof(MenuReviewId));
        }
    }
```
Our AggregateRoot class includes several methods for adding related entities to the root. 
These methods include RelatedEntity(string EntityName), RelatedEntityStronglyTypedID(string EntityName) and RelatedEntityID(string EntityName), and they can be used to register related entities, strongly typed IDs, or simple IDs under a specific name (EntityName) that can later be used to retrieve the data. Once registered, these related entities can be added to the root using the Add and AddRange methods, providing a flexible and convenient way to work with complex entity relationships in your application.<br/>
Our AggregateRoot class is designed to handle a large number of related entities, whether they are strongly typed IDs or simple IDs, depending on the needs of your application.<br/>
```
RelatedEntity("MenuSection"))
                .AddRange(sections)
                .Add(new MenuSection());
RelatedEntityID("MenuReviewId");
                .Add(Guid.NewGuid())
                .Add(Guid.NewGuid())
                .Add(Guid.NewGuid())
                .Add(Guid.NewGuid());
RelatedEntityStronglyTypedID("DinnerId")
                .Add(new DinnerId())
                .Add(new DinnerId())
                .Add(new DinnerId());
```
To retrieve the related entities, strongly typed IDs, or simple IDs that were registered with our AggregateRoot under a given name (EntityName), we provide several methods: GetEntities<Tentity>(string EntityName), GetEntityStronglyTypedIDs<TstronglytypedID>(string EntityName), and GetEntityIDs<Tid>(string EntityName). These methods return a list of entities (or IDs) of the specified type, allowing you to access the related data stored in the aggregate root and work with it as needed in your application.<br/>
```
public IReadOnlyList<MenuSection> Sections => GetEntities<MenuSection>("MenuSection").AsReadOnly();
public IReadOnlyList<DinnerId> DinnerIds => GetEntityStronglyTypedIDs<DinnerId>("DinnerId").AsReadOnly();
public IReadOnlyList<Guid> MenuReviewIds => GetEntityIDs<Guid>("MenuReviewId").AsReadOnly();
```
