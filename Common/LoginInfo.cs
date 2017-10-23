using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo {

        /// <summary>
        /// 内网ip
        /// </summary>
        public string IPv4 { get; set; }

        /// <summary>
        /// 外网ip
        /// </summary>
        public string ExtranetIP { get; set; }

        /// <summary>
        /// ip对应城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 网络运营商
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 主机物理路径
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public string System { get; set; }
    }
}
