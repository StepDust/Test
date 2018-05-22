using System;

namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 参数校验特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseValidateAttribute : Attribute {
        public string Remark { get; set; }
        public abstract MsgRes Validate(object oVal);

    }
    
}