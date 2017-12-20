using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


    public class ConvertObject
    {
        /// <summary>
        /// 将key-value类型转换成实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static T ConvertToDirObject<T>(Dictionary<string, string> dir) where T : new()
        {
            T t = new T();
            Type type = typeof(T);
            //定义一个临时变量 
            string tempName = string.Empty;
            foreach (var item in dir)
            {
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (item.Key == tempName)
                    {
                        object value = item.Value;
                        pi.SetValue(t, value,null);
                    }
                }
            }
            return t;
        }
    }

