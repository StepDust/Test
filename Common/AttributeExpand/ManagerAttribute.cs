using System;
using System.Reflection;
using Common.AttributeExpand.Validates;

namespace Common.AttributeExpand {
    public static class ManagerAttribute {

        /// <summary>
        /// 特性操作
        /// </summary>
        /// <typeparam name="BaseAttribute">指定特性</typeparam>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TResult">返回值</typeparam>
        /// <param name="model"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult AttributeAction<BaseAttribute, T, TResult>
            (T model, Func<PropertyInfo, object, TResult> func) {

            Type type = typeof(T);

            // 获取对象所有属性
            foreach (var pro in type.GetProperties()) {
                // 判断属性是否包含验证特性
                if (pro.IsDefined(typeof(BaseAttribute), true)) {
                    // 遍历所有验证验证特性
                    foreach (var item in pro.GetCustomAttributes(typeof(BaseAttribute), true)) {
                        func.Invoke(pro, item);
                    }
                }
            }
            return default;
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MsgRes Validate<T>(T model) {

            return AttributeAction<BaseValidateAttribute, T, MsgRes>(model, (pro, attribute) =>
            {
                // 校验数值
                MsgRes result = ((BaseValidateAttribute)attribute).Validate(pro.GetValue(model));

                if (result.State == 0)
                    result.Msg = $"属性：{pro.Name}，" + result.Msg;

                return result;
            });
        }

    }
}
