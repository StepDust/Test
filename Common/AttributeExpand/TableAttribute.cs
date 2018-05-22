using Interfaces.AttributeExpand;
using System;
using System.Collections.Generic;

namespace Common.AttributeExpand {

    /// <summary>
    /// 数据表描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TableAttribute : Attribute, ITableAttribute {

        public string Name { get; set; }
        public string Remark { get; set; }

        public TableAttribute(string tableName) {
            this.Name = tableName;
        }

    }
}
