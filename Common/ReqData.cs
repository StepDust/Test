using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Common {

    /// <summary>
    /// 用于接收前台传过来的数据
    /// </summary>
    public class ReqData<T> : PageInfo {

        #region 页面参数

        /// <summary>
        /// 导航栏标题
        /// </summary>
        public string Title {
            get { return CheckStr(ref title); }
            set { title = value; }
        }
        private string title;

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> DataList {
            get {
                if (dataList == null)
                    dataList = new List<T>();
                return dataList;
            }
            set { dataList = value; }
        }
        private List<T> dataList;

        /// <summary>
        /// 下拉框列表
        /// </summary>
        public List<SelectListItem> DropList {
            get {
                if (dropList == null)
                    dropList = new List<SelectListItem>();
                return dropList;
            }
            set { dropList = value; }
        }
        private List<SelectListItem> dropList;
        
        #endregion

        #region 查询参数

        /// <summary>
        /// 编号
        /// </summary>
        public int? ID {
            get { return id; }
            set { id = value; }
        }
        private int? id;

        /// <summary>
        /// 消息ID
        /// </summary> 
        public string MsgID {
            get { return CheckStr(ref msgid); }
            set { msgid = value; }
        }
        private string msgid;

        /// <summary>
        /// 消息文本
        /// </summary> 
        public string MsgStr {
            get => CheckStr(ref msgstr);
            set { msgstr = value; }
        }
        private string msgstr;

        #endregion

    }

}