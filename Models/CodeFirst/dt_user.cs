using Common.AttributeExpand.Validates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CodeFirst {

    /// <summary>
    /// 用户表
    /// </summary>
    public class DT_User {

        public int Id { get; set; }

        [RequirdVailDateAttribute]// 非空
        [LengthVailDateAttribute(1, 6)]// 长度范围
        public string Name { get; set; }

        [RangeVailDateAttribute(0, 150)]// 数字范围
        public int Age { get; set; }
        
        public DateTime? CreateTime { get; set; }
    }
}
