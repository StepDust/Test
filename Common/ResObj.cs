using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResObj {

        #region 构造函数

        public ResObj() { }

        public ResObj(int state, string msg) {
            State = state;
            Msg = msg;
        }

        public ResObj(int state, object obj) {
            State = state;
            Obj = obj;
        }

        #endregion

        #region 结果信息

        /// <summary>
        /// 状态，
        /// 规定：[-1：初始化]，[0：失败]，[1：成功]，[2：信息]，[3：帮助]，[4：锁定]，[5：开心]，[6：意外错误]
        /// </summary>
        public int? State {
            get { return DataCheck.GetNumInMinToMax(ref state, -1, 6); }
            set { state = value; }
        }
        private int? state;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        public object Obj { get; set; }

        #endregion

        #region 弹框信息

        #region 配置数据

        /// <summary>
        /// 动画，
        /// 规定：[-1：随机]，[0：变大]，[1：向下]，[2：向上]，[3：向右]，[4：风车]，[5：渐显]，[6：抖动]
        /// </summary>
        public int? Layer_Anim {
            get { return DataCheck.GetNumInMinToMax(ref anim, -1, 6); }
            set { anim = value; }
        }
        private int? anim;

        /// <summary>
        /// 时间
        /// </summary>
        public int? Layer_Time { get; set; }

        /// <summary>
        /// 皮肤
        /// </summary>
        public static string layer_Skin {
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
        private static string skin;

        #endregion

        /// <summary>
        /// 返回页面消息弹框
        /// </summary>
        /// <returns></returns>
        public string GetMsg() {
            return LayerMsg(Msg, Url, GetIcon(State ?? -1), Layer_Anim ?? -1, Layer_Time ?? 2000);
        }

        /// <summary>
        /// 返回页面消息弹框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="url">跳转链接</param>
        /// <param name="icon">消息类型</param>
        /// <param name="anim">消息动画</param>
        /// <param name="time">出现时间</param>
        /// <returns></returns>
        public static string LayerMsg(string msg, string url = "", Icon icon = Icon.None, int anim = -1, int time = 2000) {

            // 链接
            if (string.IsNullOrEmpty(url))
                url = Utils.GetUrlInfo();
            // 动画
            if (anim < 0)
                anim = new Random().Next(0, 7);

            StringBuilder str = new StringBuilder();

            object[] key = { "skin", "anim", "time", "icon" };
            object[] val = { layer_Skin, anim, time, GetIconNum(icon) };

            str.Append("<script>");
            str.Append("loaction.href='" + url + "'; ");
            str.Append("layer.msg('" + msg + "'{");
            str.Append(Utils.MosaicKeyVal(key, val) + "});");
            str.Append("</script>");

            return str.ToString();
        }

        #endregion

        #region 字典转换

        /// <summary>
        /// 返回Icon对应的数字，
        /// 大量强制转换会有损性能
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static int? GetIconNum(Icon icon) {
            switch (icon) {
                case Icon.Info: return 0;
                case Icon.Success: return 1;
                case Icon.Error: return 2;
                case Icon.Help: return 3;
                case Icon.Lock: return 4;
                case Icon.Happy: return 5;
                case Icon.Sorry: return 6;
                default: return null;
            }
        }

        /// <summary>
        /// 根据状态返回对应枚举
        /// </summary>
        /// <param name="state">信息状态</param>
        /// <returns></returns>
        public static Icon GetIcon(int state) {
            state = state % 7;
            switch (state) {
                // 失败
                case 0: return Icon.Error;
                // 成功
                case 1: return Icon.Success;
                // 信息
                case 2: return Icon.Info;
                // 帮助
                case 3: return Icon.Help;
                // 锁定
                case 4: return Icon.Lock;
                // 开心
                case 5: return Icon.Happy;
                // 难过
                case 6: return Icon.Sorry;
                // 暂无
                default: return Icon.None;
            }
        }

        #endregion

    }

    public enum Icon {
        /// <summary>
        /// 暂无
        /// </summary>
        None = -1,
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
