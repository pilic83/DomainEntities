using System.Reflection;

namespace DomainEntities.Helpers
{
    internal class RandomClassGenerator<Tclass>
        where Tclass : class, new()
    {
        public Tclass randomObject { get; private set; }

        public RandomClassGenerator()
        {
            var obj = new Tclass();
            var type = typeof(Tclass);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                try
                {
                    property.SetValue(obj, default);
                } catch { }
                
                if (!propertyType.IsArray)
                {
                    MenageArraylessTypes(propertyType, property, obj);
                }
                if (propertyType.IsArray)
                {
                    Array value = Array.CreateInstance(propertyType.GetElementType()!, 3 + Random.Shared.Next(3));
                    for(int i = 0; i < value.Length; i++)
                    {
                        value.SetValue(default!, i);
                        if (propertyType == typeof(int[])) value.SetValue(Random.Shared.Next(), i);
                        if (propertyType == typeof(uint[])) value.SetValue((uint)Random.Shared.NextInt64(uint.MaxValue), i);
                        if (propertyType == typeof(long[])) value.SetValue(Random.Shared.NextInt64(), i);
                        if (propertyType == typeof(ulong[])) value.SetValue((ulong)Random.Shared.NextInt64(), i);
                        if (propertyType == typeof(short[])) value.SetValue((short)Random.Shared.Next(short.MaxValue), i);
                        if (propertyType == typeof(ushort[])) value.SetValue((ushort)Random.Shared.Next(ushort.MaxValue), i);
                        if (propertyType == typeof(sbyte[])) value.SetValue((sbyte)Random.Shared.Next(sbyte.MaxValue), i);
                        if (propertyType == typeof(byte[])) value.SetValue((byte)Random.Shared.Next(byte.MaxValue), i);
                        if (propertyType == typeof(char[])) value.SetValue(Path.GetRandomFileName().Replace(".", "")[0], i);
                        if (propertyType == typeof(double[])) value.SetValue(Random.Shared.NextDouble() * (double.MaxValue / (double)(1 << 20)), i);
                        if (propertyType == typeof(float[])) value.SetValue((float)Random.Shared.NextDouble() * (float.MaxValue / (float)(1 << 20)), i);
                        if (propertyType == typeof(decimal[])) value.SetValue((decimal)Random.Shared.NextDouble() * (decimal.MaxValue / (decimal)(1 << 30)), i);
                        if (propertyType == typeof(bool[])) value.SetValue(Random.Shared.Next(2) == 0, i);
                        if (propertyType == typeof(string[])) value.SetValue(Path.GetRandomFileName().Replace(".", ""), i); 
                        if (propertyType.GetElementType()!.IsClass && propertyType.GetElementType() != typeof(string) && !propertyType.GetElementType()!.IsEnum)
                        {
                            Type genericType = typeof(RandomClassGenerator<>).MakeGenericType(propertyType.GetElementType()!);
                            var valueGenerator = Activator.CreateInstance(genericType);
                            var valueClass = valueGenerator!.GetType()
                                .GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList()
                                .Find(prop => prop.Name.Equals("randomObject"))!
                                .GetValue(valueGenerator);
                            value.SetValue(valueClass, i);
                        }
                    }
                    property.SetValue(obj, value);
                }
            }

            randomObject = obj;
            
        }

        public static Tclass GetRandomObject()
        {
            var rand = new RandomClassGenerator<Tclass>();
            return rand.randomObject;
        }

        private void MenageArraylessTypes(Type propertyType, PropertyInfo property, Tclass obj)
        {
            try 
            { 
                property.SetValue(obj, default);
            }
            catch { }
            if (propertyType == typeof(string)) SetValue<string>(Path.GetRandomFileName().Replace(".", ""), property, obj);
            if (propertyType.IsValueType) MenageValueType(propertyType, property, obj);
            if (propertyType.IsEnum) property.SetValue(obj, default);
            if (propertyType.IsClass && propertyType != typeof(string) && !propertyType.IsEnum)
            {
                try
                {
                    Type genericType = typeof(RandomClassGenerator<>).MakeGenericType(propertyType);
                    var valueGenerator = Activator.CreateInstance(genericType);
                    var value = valueGenerator!.GetType()
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList()
                                    .Find(prop => prop.Name.Equals("randomObject"))!
                                    .GetValue(valueGenerator);
                    property.SetValue(obj, value);
                }
                catch { }
            }
        }

        private void MenageValueType(Type propertyType, PropertyInfo property, Tclass obj)
        {
            try
            {
                property.SetValue(obj, default);
            }
            catch { }
            if (propertyType == typeof(int)) SetValue<int>(Random.Shared.Next(), property, obj);
            if (propertyType == typeof(uint)) SetValue<uint>((uint)Random.Shared.NextInt64(uint.MaxValue), property, obj);
            if (propertyType == typeof(long)) SetValue<long>(Random.Shared.NextInt64(), property, obj);
            if (propertyType == typeof(ulong)) SetValue<ulong>((ulong)Random.Shared.NextInt64(), property, obj);
            if (propertyType == typeof(short)) SetValue<short>((short)Random.Shared.Next(short.MaxValue), property, obj);
            if (propertyType == typeof(ushort)) SetValue<ushort>((ushort)Random.Shared.Next(ushort.MaxValue), property, obj);
            if (propertyType == typeof(sbyte)) SetValue<sbyte>((sbyte)Random.Shared.Next(sbyte.MaxValue), property, obj);
            if (propertyType == typeof(byte)) SetValue<byte>((byte)Random.Shared.Next(byte.MaxValue), property, obj);
            if (propertyType == typeof(char)) SetValue<char>(Path.GetRandomFileName().Replace(".", "")[0], property, obj);
            if (propertyType == typeof(double)) SetValue<double>(Random.Shared.NextDouble() * (double.MaxValue / (double)(1 << 20)), property, obj);
            if (propertyType == typeof(float)) SetValue<float>((float)Random.Shared.NextDouble() * (float.MaxValue / (float)(1 << 20)), property, obj);
            if (propertyType == typeof(decimal)) SetValue<decimal>((decimal)Random.Shared.NextDouble() * (decimal.MaxValue / (decimal)(1 << 30)), property, obj);
            if (propertyType == typeof(bool)) SetValue<bool>(Random.Shared.Next(2) == 0, property, obj);
        }

        private void SetValue<T>(T value, PropertyInfo property, Tclass obj)
        {
            try 
            { 
                property.SetValue(obj, value);
            }
            catch { }
        }
    }
}
