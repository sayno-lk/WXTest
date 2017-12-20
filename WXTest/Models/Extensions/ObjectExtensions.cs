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
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;


public static class ObjectExtensions
{

    public static string ToJson(this object obj)
    {
        return ToJson(obj, null);
    }
    public static string ToJson(this object obj, IEnumerable<JavaScriptConverter> jsonConverters)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        if (jsonConverters != null) serializer.RegisterConverters(jsonConverters ?? new JavaScriptConverter[0]);
        return serializer.Serialize(obj);
    }

    public static T ConvertTo<T>(this object value) { return value.ConvertTo(default(T)); }
    public static T ConvertTo<T>(this object value, T defaultValue)
    {
        if (value != null)
        {
            var targetType = typeof(T);

            var converter = TypeDescriptor.GetConverter(value);
            if (converter != null)
            {
                if (converter.CanConvertTo(targetType)) return (T)converter.ConvertTo(value, targetType);
            }

            converter = TypeDescriptor.GetConverter(targetType);
            if (converter != null)
            {
                try { if (converter.CanConvertFrom(value.GetType())) return (T)converter.ConvertFrom(value); } catch { }
            }
        }
        return defaultValue;
    }
    public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
    {
        if (ignoreException)
        {
            try
            {
                return value.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }
        return value.ConvertTo<T>();
    }

    public static int ToInt(this object strValue, int defValue) { int def = 0; int.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static byte ToTinyInt(this object strValue, byte defValue) { byte def = 0; byte.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static short ToSmallInt(this object strValue, short defValue) { short def = 0; short.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static decimal ToDecimal(this object strValue, decimal defValue) { decimal def = 0; decimal.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static float ToFloat(this object strValue, float defValue) { float def = 0; float.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static Int64 ToBigInt(this object strValue, Int64 defValue) { Int64 def = 0; Int64.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static decimal ToMoney(this object strValue, decimal defValue) { decimal def = 0; decimal.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static int ToInteger(this object strValue, int defValue) { int def = 0; int.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
    public static bool ToBool(this object Expression, bool defValue)
    {
        if (Expression != null)
        {
            if (string.Compare(Expression.ToString(), "true", true) == 0) return true;
            if (string.Compare(Expression.ToString(), "false", true) == 0) return false;
            if (string.Compare(Expression.ToString(), "1", true) == 0) return true;
            if (string.Compare(Expression.ToString(), "0", true) == 0) return false;
        }
        return defValue;
    }
    public static int ToInt(this object strValue) { return strValue.ToInt(0); }
    public static byte ToTinyInt(this object strValue) { return strValue.ToTinyInt(0); }
    public static short ToSmallInt(this object strValue) { return strValue.ToSmallInt(0); }
    public static decimal ToDecimal(this object strValue) { return strValue.ToDecimal(0); }
    public static float ToFloat(this object strValue) { return strValue.ToFloat(0); }
    public static Int64 ToBigInt(this object strValue) { return strValue.ToBigInt(0); }
    public static decimal ToMoney(this object strValue) { return strValue.ToMoney(0); }
    public static int ToInteger(this object strValue) { return strValue.ToInteger(0); }
    public static bool ToBool(this object strValue) { return strValue.ToBool(false); }

    public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
    {
        return InvokeMethod<object>(obj, methodName, parameters);
    }
    public static T InvokeMethod<T>(this object obj, string methodName)
    {
        return InvokeMethod<T>(obj, methodName, null);
    }
    public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
    {
        var type = obj.GetType();
        var method = type.GetMethod(methodName);

        if (method == null) throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);

        var value = method.Invoke(obj, parameters);
        return (value is T ? (T)value : default(T));
    }

    public static object GetPropertyValue(this object obj, string propertyName)
    {
        return GetPropertyValue<object>(obj, propertyName, null);
    }
    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
        return GetPropertyValue<T>(obj, propertyName, default(T));
    }
    public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);

        if (property == null) throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);

        var value = property.GetValue(obj, null);
        return (value is T ? (T)value : defaultValue);
    }
    public static void SetPropertyValue(this object obj, string propertyName, object value)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);

        if (property == null) throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);

        property.SetValue(obj, value, null);
    }

    public static T GetAttribute<T>(this object obj) where T : Attribute
    {
        return GetAttribute<T>(obj, true);
    }
    public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
    {
        var type = (obj as Type ?? obj.GetType());
        var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
        if ((attributes != null) && (attributes.Length > 0))
        {
            return (attributes[0] as T);
        }
        return null;
    }

    public static IEnumerable<T> GetAttributes<T>(this object obj) where T : Attribute
    {
        return GetAttributes<T>(obj);
    }
    public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
    {
        var type = (obj as Type ?? obj.GetType());
        foreach (var attribute in type.GetCustomAttributes(typeof(T), includeInherited))
        {
            if (attribute is T) yield return (T)attribute;
        }
    }

    public static bool IsType(this object obj, Type type)
    {
        return obj.GetType().Equals(type);
    }
    public static T ToType<T>(this object value) { return (T)value; }
    public static bool IsArray(this object obj)
    {
        return obj.IsType(typeof(System.Array));
    }
    public static bool IsDBNull(this object obj)
    {
        return obj.IsType(typeof(DBNull));
    }

    public static byte[] Serialize(this object value)
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf1 = new BinaryFormatter();
        bf1.Serialize(ms, value);
        return ms.ToArray();
    }

    public static void CheckOnNull(this object @this, string parameterName)
    {
        if (@this.IsNull()) throw new ArgumentNullException(parameterName);
    }
    public static void CheckOnNull(this object @this, string parameterName, string message)
    {
        if (@this.IsNull()) throw new ArgumentNullException(parameterName, message);
    }
    public static bool IsNull(this object @this)
    {
        return @this == null;
    }
    public static bool IsNotNull(this object @this)
    {
        return !@this.IsNull();
    }
    public static T UnsafeCast<T>(this object value)
    {
        return value.IsNull() ? default(T) : (T)value;
    }
    public static T SafeCast<T>(this object value)
    {
        return value is T ? value.UnsafeCast<T>() : default(T);
    }
    public static bool InstanceOf<T>(this object value)
    {
        return value is T;
    }

}
