using System;
using System.Reflection;

namespace Common {
    /// <summary>
    /// 反射类
    /// </summary>
    public static class Reflex {

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
        /// 通过反射创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dllName">dll绝对路径</param>
        /// <param name="className">class完整名称</param>
        /// <returns></returns>
        public static T CreateModel<T>(string dllName, string className) {
            // 反射加载DLL
            Assembly assembly = Assembly.LoadFile(dllName);
            // 从dll中获取指定class
            Type type = assembly.GetType(className);

            return CreateModel<T>(type);
        }

        /// <summary>
        /// 根据Type创建实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CreateModel<T>(Type type = null) {

            if (type == null) type = typeof(T);

            // 判断指定class是否为泛型
            if (type.IsGenericTypeDefinition) {
                // 获取所需泛型数组
                Type[] typeArr = typeof(T).GenericTypeArguments;
                type = type.MakeGenericType(typeArr);
            }

            // 根据DLL，寻找类，并创建
            T model = (T)Activator.CreateInstance(type);
            return model;
        }

    }
}
