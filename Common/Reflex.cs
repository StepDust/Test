using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 反射类
    /// </summary>
    public class Reflex {

        /// <summary>
        /// 根据字符串返回对应的属性值
        /// </summary>
        /// <param name="obj">指定对象</param>
        /// <param name="Field">指定属性</param>
        /// <returns></returns>
        public static s GetValByField<s>(object obj, string Field, s def = default)
            where s : class, new()
        {
            Type type = obj.GetType();
            PropertyInfo F = type.GetProperty(Field);
            if (F == null)
                return def;
            return F.GetValue(obj) as s;
        }

        /// <summary>
        /// 将两个对象属性名称相同的值进行拷贝
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="Res">返回的对象</param>
        /// <param name="Data">提供数据的对象</param>
        /// <returns></returns>
        public static T1 CopyVal<T1, T2>(T1 Res, T2 Data)
            where T1 : class, new()
        {
            Type resType = Res.GetType();
            Type dataType = Data.GetType();

            PropertyInfo[] info = resType.GetProperties();

            foreach (var item in info) {
                PropertyInfo temp = dataType.GetProperty(item.Name);
                if (temp == null)
                    break;

                object val = temp.GetValue(Data);
                item.SetValue(Res, val);
            }

            return Res;
        }

    }
}
