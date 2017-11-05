using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace EBuy {

    public class Manager : Controller {

        /// <summary>
        /// 设置Response状态
        /// </summary>
        /// <param name="Code">状态码</param>
        /// <param name="Content">状态消息，只能输出英文</param>
        public void SetResState(int Code, string Content)
        {
            Response.StatusCode = Code;
            Response.StatusDescription = Content;
        }

        /// <summary>
        /// 返回计算结果
        /// </summary>
        /// <param name="str"></param>
        public void ResWrite(string str)
        {

            try {
                Icon i = Icon.Success;
                if (string.IsNullOrEmpty(str)) {
                    str = "请输入正确的数据！";
                    i = Icon.Error;
                }


                ResObj res = new ResObj("返回结果", str, i);

                Response.Write(Utils.ObjectToJson(res));
            }
            catch (Exception e) {
                ResObj res = new ResObj("错误信息", e.StackTrace + "报错，" + e.Message, Icon.Error);
                Response.Write(Utils.ObjectToJson(res));
            }
        }

        #region 返回消息

        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="url">跳转链接</param>
        /// <param name="isEval">是否需要eval执行</param>
        /// <returns></returns>
        public ActionResult Error(string msg, string url = "Index", bool isEval = true)
        {
            url = Utils.GetActionUrl(url);
            if (isEval)
                return Content(ResObj.LayerScript(msg, Icon.Error, url));
            return Content(ResObj.LayerMsg(msg, Icon.Error, url));
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="url">跳转链接</param>
        /// <param name="isEval">是否需要eval执行</param>
        /// <returns></returns>
        public ActionResult Success(string msg, string url = "Index", bool isEval = true)
        {
            url = Utils.GetActionUrl(url);
            if (isEval)
                return Content(ResObj.LayerScript(msg, Icon.Success, url));
            return Content(ResObj.LayerMsg(msg, Icon.Success, url));
        }



        #endregion

    }
}