using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBuy {
    /// <summary>
    /// 导航栏数据
    /// </summary>
    [Serializable]
    public class NavData {

        public NavData()
        {
            ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 所在层级
        /// </summary>
        public int Layer { get; set; }
        /// <summary>
        /// 下级数据
        /// </summary>
        public List<NavData> ChildrenList { get; set; }
    }
}