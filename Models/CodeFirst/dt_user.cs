using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CodeFirst {

    /// <summary>
    /// 用户表
    /// </summary>
    public class dt_user {

        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
