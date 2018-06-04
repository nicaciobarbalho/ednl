using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EDNL
{
    public static class Extensions
    {
        public static T Clone<T>(this T value) where T : class
        {
            string json = JsonConvert.SerializeObject(value);
            Type tipo = value.GetType();
            object obj = JsonConvert.DeserializeObject<T>(json);

            return (T)Convert.ChangeType(obj, tipo);
            //return CloneHelper<T>.Clone(value);

        }

        public static class CloneHelper<T> where T : class
        {
            private static readonly Lazy<PropertyHelper.Accessor<T>[]> _LazyCloneableAccessors =
                new Lazy<PropertyHelper.Accessor<T>[]>(CloneableProperties, isThreadSafe: true);

            private static readonly Func<object, object> MemberwiseCloneFunc;

            static CloneHelper()
            {
                var flags = BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic;
                MemberwiseCloneFunc = (Func<object, object>)Delegate.CreateDelegate(
                    typeof(Func<object, object>),
                    typeof(T).GetMethod("MemberwiseClone", flags));
            }

            [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
            private static PropertyHelper.Accessor<T>[] CloneableProperties()
            {
                var bindingFlags = BindingFlags.Instance
                                   | BindingFlags.FlattenHierarchy
                                   | BindingFlags.Public
                                   | BindingFlags.NonPublic;

                var result = typeof(T)
                    .GetProperties(bindingFlags)
                    .Where(p => p.PropertyType != typeof(string) && !p.PropertyType.IsValueType)
                    .Where(p => p.PropertyType.GetMethods(bindingFlags).Any(x => x.Name == "Clone"))
                    .Select(PropertyHelper.CreateAccessor<T>)
                    .Where(a => a != null)
                    .ToArray();

                return result;
            }

            public static T Clone(T objToClone)
            {
                var clone = MemberwiseCloneFunc(objToClone) as T;

                // clonando todas as propriedades que possuem um método Clone
                foreach (var accessor in _LazyCloneableAccessors.Value)
                {
                    var propToClone = accessor.GetValueObj(objToClone);
                    var clonedProp = propToClone == null ? null : ((dynamic)propToClone).Clone() as object;
                    accessor.SetValueObj(objToClone, clonedProp);
                }

                return clone;
            }

        }

        public static class PropertyHelper
        {
            // solução baseada em: http://stackoverflow.com/questions/4085798/creating-an-performant-open-delegate-for-an-property-setter-or-getter

            public abstract class Accessor<T>
            {
                public abstract void SetValueObj(T obj, object value);
                public abstract object GetValueObj(T obj);
            }

            public class Accessor<TTarget, TValue> : Accessor<TTarget>
            {
                private readonly PropertyInfo _property;
                public Accessor(PropertyInfo property)
                {
                    _property = property;

                    if (property.GetSetMethod(true) != null)
                        this.Setter = (Action<TTarget, TValue>)
                            Delegate.CreateDelegate(typeof(Action<TTarget, TValue>), property.GetSetMethod(true));

                    if (property.GetGetMethod(true) != null)
                        this.Getter = (Func<TTarget, TValue>)
                        Delegate.CreateDelegate(typeof(Func<TTarget, TValue>), property.GetGetMethod(true));
                }

                public Action<TTarget, TValue> Setter { get; private set; }
                public Func<TTarget, TValue> Getter { get; private set; }

                public override void SetValueObj(TTarget obj, object value) { Setter(obj, (TValue)value); }
                public override object GetValueObj(TTarget obj) { return Getter(obj); }
                public override string ToString() { return _property.ToString(); }
            }

            public static Accessor<T> CreateAccessor<T>(PropertyInfo property)
            {
                var getMethod = property.GetGetMethod();
                if (getMethod == null || getMethod.GetParameters().Length != 0)
                    return null;
                var accessor = (Accessor<T>)Activator.CreateInstance(
                    typeof(Accessor<,>).MakeGenericType(typeof(T),
                        property.PropertyType), property);
                return accessor;
            }

            public static Accessor<TIn, TOut> CreateAccessor<TIn, TOut>(PropertyInfo property)
            {
                return (Accessor<TIn, TOut>)CreateAccessor<TIn>(property);
            }
        }
    }
}
