using System;

namespace Common.AttributeExpand {

    /// <summary>
    /// 数据表描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TableAttribute : Attribute {

        public string Name { get; set; }
        public string Remark { get; set; }

        public TableAttribute(string tableName) {
            this.Name = tableName;
        }

    }
}
