using System;
using System.Reflection;

namespace Common {
    /// <summary>
    /// 反射类
    /// </summary>
    public class Reflex {

        /// <summary>
        /// 根据字符串返回对应的属性值
        /// </summary>
        /// <param name="obj">指定对象</param>
        /// <param name="field">指定属性</param>
        /// <returns></returns>
        public static s GetValByField<s>(object obj, string field, s def = default)
            where s : class, new() {
            Type type = obj.GetType();
            PropertyInfo F = type.GetProperty(field);
            if (F == null)
                return def;
            return F.GetValue(obj) as s;
        }

        /// <summary>
        /// 将两个对象属性名称相同的值进行拷贝
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="res">返回的对象</param>
        /// <param name="data">提供数据的对象</param>
        /// <returns></returns>
        public static T1 CopyVal<T1, T2>(T1 res, T2 data)
            where T1 : class, new() {
            Type resType = res.GetType();
            Type dataType = data.GetType();

            PropertyInfo[] info = resType.GetProperties();

            foreach (var item in info) {
                PropertyInfo temp = dataType.GetProperty(item.Name);
                if (temp == null)
                    break;

                object val = temp.GetValue(data);
                item.SetValue(res, val);
            }

            return res;
        }

        /// <summary>
        /// 通过反射创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static T CreateModel<T>(string dllName, string className) where T : class {
            // 反射加载DLL
            Assembly assembly = Assembly.Load(dllName);

            Type type = assembly.GetType(className);

            Type[] typeArr = typeof(T).GenericTypeArguments;
            if (typeArr.Length > 0)
                type = type.MakeGenericType(typeArr);

            // 根据DLL，寻找类，并创建
            T model = (T)Activator.CreateInstance(type);
            return model;
        }

    }
}
