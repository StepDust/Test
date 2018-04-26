using Interfaces;
using Interfaces.AttributeExpand;
using System;

namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 参数校验特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseValidateAttribute : Attribute, IBaseValidateAttribute {
        public string Remark { get; set; }
        public abstract IMessage Validate(object oVal);

    }

    public class DataValidate {
        public static IMessage Validate<T>(T obj) {
            Type type = obj.GetType();

            IMessage result = new Message(0, "");

            // 获取对象所有属性
            foreach (var pro in type.GetProperties()) {

                // 判断属性是否包含验证特性
                if (pro.IsDefined(typeof(BaseValidateAttribute), true)) {

                    IBaseValidateAttribute attribute;

                    // 遍历所有验证验证特性
                    foreach (var item in pro.GetCustomAttributes(typeof(BaseValidateAttribute), true)) {
                        attribute = item as IBaseValidateAttribute;

                        // 校验数值
                        result = attribute.Validate(pro.GetValue(obj));

                        if (result.State == 0) {
                            result.Msg = $"属性：{pro.Name}，" + result.Msg;
                            return result;
                        }
                    }
                }
            }
            return result;
        }
    }
}