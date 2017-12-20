using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Web.Script.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.ObjectModel;


public static class IEnumerableExtensions
{

    public static bool IsNullOrEmpty(this IEnumerable source)
    {
        if (source == null) return true;
        foreach (var item in source) return false;
        return true;
    }
    public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
    {
        DataTable dtReturn = new DataTable();
        PropertyInfo[] oProps = null;
        if (varlist == null) return dtReturn;

        foreach (T rec in varlist)
        {
            if (oProps == null)
            {
                oProps = ((Type)rec.GetType()).GetProperties();
                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) colType = colType.GetGenericArguments()[0];
                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
            }
            DataRow dr = dtReturn.NewRow();
            foreach (PropertyInfo pi in oProps) dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
            dtReturn.Rows.Add(dr);
        }
        return dtReturn;
    }
    public static List<TSource> ToPage<TSource>(this IEnumerable<TSource> varlist, int pageIndex, int pageSize, out int totalRecords)
    {
        totalRecords = varlist.Count();
        return varlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    }
    public static string ToJson<TSource, TResult>(this IEnumerable<TSource> varlist, Func<TSource, TResult> selector)
    {
        return varlist.Select(selector).ToJson();
    }

    public static IEnumerable<TTarget> ConvertList<TSource, TTarget>(this IEnumerable<TSource> source)
    {
        foreach (var value in source)
        {
            yield return value.ConvertTo<TTarget>();
        }
    }
    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        foreach (var value in values)
        {
            action(value);
        }
    }
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> odd, Action<T> even)
    {
        bool isOdd = true;
        ForEach(items, item =>
        {
            if (isOdd) odd(item); else even(item);
            isOdd = !isOdd;
        });
    }
    public static IEnumerable<T> MergeWith<T>(this IEnumerable<T> target, params IEnumerable<T>[] data)
    {
        List<T> list = new List<T>(target);
        data.ForEach(array => list.AddRange(array));
        return list;
    }
    public static IEnumerable<T> With<T>(this IEnumerable<T> source, T item)
    {
        foreach (T t in source) yield return t;
        yield return item;
    }
    public static bool ContainsAtLeast<T>(this IEnumerable<T> enumeration, int count)
    {
        if (enumeration == null) throw new ArgumentNullException("enumeration");
        return (from t in enumeration.Take(count) select t).Count() == count;
    }
    public static IEnumerable<T[]> GroupEvery<T>(this IEnumerable<T> enumeration, int count)
    {
        if (enumeration == null) throw new ArgumentNullException("IEnumerable cannot be null.");
        if (count <= 0) throw new ArgumentOutOfRangeException("The count parameter must be greater than zero.");
        int current = 0;
        T[] array = new T[count];
        foreach (var item in enumeration)
        {
            array[current++] = item;
            if (current == count)
            {
                yield return array;
                current = 0;
                array = new T[count];
            }
        }
        if (current != 0) yield return array;
    }
    public static int IndexOf<T>(this IEnumerable<T> enumeration, T value) where T : IEquatable<T>
    {
        return enumeration.IndexOf<T>(value, EqualityComparer<T>.Default);
    }
    public static int IndexOf<T>(this IEnumerable<T> enumeration, T value, IEqualityComparer<T> comparer)
    {
        int index = 0;
        foreach (var item in enumeration)
        {
            if (comparer.Equals(item, value)) return index;
            index++;
        }
        return -1;
    }
    public static int IndexOf<T>(this IEnumerable<T> enumeration, T value, int startIndex) where T : IEquatable<T>
    {
        return enumeration.IndexOf<T>(value, startIndex, EqualityComparer<T>.Default);
    }
    public static int IndexOf<T>(this IEnumerable<T> enumeration, T value, int startIndex, IEqualityComparer<T> comparer)
    {
        for (int i = startIndex; i < enumeration.Count(); i++)
        {
            T item = enumeration.ElementAt(i);
            if (comparer.Equals(item, value)) return i;
        }
        return -1;
    }
    public static int IndexOfPrevious<T>(this IEnumerable<T> items, T value, int fromIndex)
    {
        for (int i = fromIndex - 1; i > -1; i--)
        {
            T item = items.ElementAt(i);
            if (EqualityComparer<T>.Default.Equals(item, value)) return i;
        }
        return -1;
    }
    public static bool Contains<T>(this IEnumerable<T> items, T value)
    {
        if (items == null) throw new ArgumentNullException("items");

        ICollection<T> c = items as ICollection<T>;
        if (c != null) return c.Contains(value);

        throw new NotImplementedException();
    }

    public static T Constructor<T>(this IEnumerable<object> Parameters, IDictionary<string, object> Properties) where T : class
    {
        Type ttype = typeof(T);
        T obj = (T)Activator.CreateInstance(typeof(T), Parameters);
        foreach (string key in Properties.Keys)
        {
            PropertyInfo prop = ttype.GetProperty(key);
            if (prop != null) prop.SetValue(obj, Properties[key], null);
        }
        return obj;
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> inputs, Func<T, IEnumerable<T>> enumerate)
    {
        if (inputs != null)
        {
            var stack = new Stack<T>(inputs);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == null) continue;
                yield return current;
                var enumerable = enumerate != null ? enumerate(current) : null;
                if (enumerable != null)
                {
                    foreach (var child in enumerable) stack.Push(child);
                }
            }
        }
    }
    public static IEnumerable Flatten(this IEnumerable inputs, Func<object, System.Collections.IEnumerable> enumerate)
    {
        return Flatten<object>(inputs.Cast<object>(), o => (enumerate(o) ?? new object[0]).Cast<object>());
    }

    public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> @this)
    {
        return new ReadOnlyCollection<T>(new List<T>(@this));
    }

    public static bool In<T>(this T source, IEnumerable<T> values) where T : IEquatable<T>
    {
        if (values == null) return false;

        foreach (T listValue in values)
        {
            if ((default(T).Equals(source) && default(T).Equals(listValue)) || (!default(T).Equals(source) && source.Equals(listValue))) return true;
        }

        return false;
    }

}

