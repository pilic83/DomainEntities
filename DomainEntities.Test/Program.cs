using DomainEntities.Test.AggregateRoots;
using DomainEntities.Test.Entities;
using DomainEntities.Test.StronglyTypedIDs;
using DomainEntities.Test.ValueObjects;
using System.Text.Json;

Price p = new Price(10, "RSD");
var q = new Price(10, "RSD");
Console.WriteLine(p.Equals(q));

BillId b = new();
Console.WriteLine(b.Value);
var bill = BillId.CreateWithID<BillId>(Guid.Empty);
Console.WriteLine(bill.Value);

MemberId m = new();
Console.WriteLine(m.Value);

var memberA = MemberId.CreateWithID<MemberId>(10);
Console.WriteLine(memberA.Value);
var memberB = MemberId.CreateWithID<MemberId>(10);
Console.WriteLine(memberA.Equals(memberB));

NameId name = new();
Console.WriteLine(name.Value);

DataId time = new();
Console.WriteLine(time.Value);
var d = DataId.CreateWithID<DataId>(DateTime.MaxValue);
Console.WriteLine(d.Value);

DecimalId dec = new();
Console.WriteLine(dec.Value);

PriceAsID objn = new();
var obj = PriceAsID.CreateWithID<PriceAsID>(p);
var obj1 = PriceAsID.CreateWithID<PriceAsID>(new Price(10, "RSD"));
Console.WriteLine(JsonSerializer.Serialize<Price>(obj.Value));
Console.WriteLine(obj1.Equals(obj));

//TestClass tClass = new();
//Console.WriteLine(JsonSerializer.Serialize<TestClass>(tClass));
//var tClassValue = TestClassAsID.CreateWithID<TestClassAsID>(tClass);
//TestClassAsID tClassNewValue = new();
//TestClassAsID tClassValue1 = TestClassAsID.CreateWithID<TestClassAsID>(tClass);
//Console.WriteLine(JsonSerializer.Serialize<TestClass>(tClassNewValue.Value));
//Console.WriteLine(tClassValue.Equals(tClassNewValue));
//Console.WriteLine(tClassValue.Equals(tClassValue1));

var billEntity = BillEntity.CreateWithID<BillEntity>(b.Value);
Console.WriteLine(billEntity.Id.Value);
var billEntity1 = BillEntity.CreateWithStronglyTypedID<BillEntity>(b);
Console.WriteLine(billEntity.Equals(billEntity1));
BillEntity billEntity2 = new();
Console.WriteLine(billEntity2.Id.Value);

var dalysales = new DailySales();
var listBillSTIDs = dalysales.BillIds;

var listTestEntities = dalysales.Tests;

var floatedIDs = dalysales.Floated;

var dalysalesNew = DailySales.CreateWithID<DailySales>(dalysales.Id.Value);
Console.WriteLine(dalysales.Equals(dalysalesNew));
Console.WriteLine(JsonSerializer.Serialize<DailySales>(dalysales));
Console.ReadLine();