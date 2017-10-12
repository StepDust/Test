using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {

    /// <summary>
    /// 消息返回
    /// </summary>
    public class ResultMsg {

        #region 弹框消息

        /// <summary>
        /// 内容，可以为任意文本或HTML
        /// </summary>
        public string layer_Content {
            get { return content ?? ""; }
            set { content = value; }
        }
        private string content;

        /// <summary>
        /// 皮肤
        /// </summary>
        public string layer_Skin {
            get {
                switch (skin) {
                    case "layui-layer-lan":
                    case "layui-layer-molv":
                        return skin;
                    default:
                        return skin = "";
                }
            }
            set { skin = value; }
        }
        private string skin;

        public string layer_Area {
            get {
                return "['500px','400px']";
            }
            set { area = value; }
        }
        private string area;

        /// <summary>
        /// 返回消息弹框
        /// </summary>
        /// <returns></returns>
        public string Msg() {

            return "";
        }

        /// <summary>
        /// 返回页面弹框
        /// </summary>
        /// <returns></returns>
        public string Open() {
            return "";
        }


        #endregion

    }

    public enum Icon {
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Error,
        /// <summary>
        /// 帮助
        /// </summary>
        Help,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock,
        /// <summary>
        /// 开心
        /// </summary>
        Happy,
        /// <summary>
        /// 难过
        /// </summary>
        Sorry
    }
}