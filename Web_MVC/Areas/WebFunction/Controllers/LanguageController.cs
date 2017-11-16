using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers {
    public class LanguageController : Controller {
        // GET: WebFunction/Language
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="path">语言包路径</param>
        /// <param name="url">翻译网址</param>
        /// <param name="Lan">翻译类型</param>
        public ActionResult Translate(string path, string url, string Lan)
        {
            string content = FileAction.ReadToStr(path);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string reg = "msgid \"(?<msgid>.*)\" \r\nmsgstr \"(?<msgstr>.*)\"";

            string[] strArr = DataCheck.GetRegStrArr(content, reg);

            foreach (var str in strArr) {
                MatchCollection res = Regex.Matches(str, reg, RegexOptions.IgnoreCase);
                foreach (Match item in res) {
                    dic.Add(item.Groups["msgid"].Value, item.Groups["msgstr"].Value);
                }
            }

            // 发送翻译请求
            Dictionary<string, string> dicRes= BaiduTranslate(dic, Lan, "");


            return View();
        }

        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="dic">字典数据</param>
        /// <param name="Lan">翻译类型</param>
        /// <param name="path">保存路径</param>
        public Dictionary<string, string> BaiduTranslate(Dictionary<string, string> dic, string Lan, string path)
        {

            string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
            string appid = "20171116000095832";
            string appkey = "PuVyBlMqMOOjqfks4GJ7";

            // 拼接翻译类型参数
            string[] lanArr = Lan.Split(';');
            url += "from=" + lanArr[0];// 翻译源语言
            url += "&to=" + lanArr[1];//  译文语言
            url += "&appid=" + appid;// 接口ID

            Dictionary<string, string> res = new Dictionary<string, string>();
            Crawler crawler = new Crawler();
            // 请求结束执行
            crawler.OnCompleted += (s, e) => {

                BaiduTransAPI tran = new BaiduTransAPI();

                string cc = Utils.ObjectToJson(tran);
                tran = Utils.JsonToObject(e.PageSource, tran) as BaiduTransAPI;

                if (tran.trans_result.Count > 0)
                    res.Add(tran.trans_result[0].src, tran.trans_result[0].dst);
                else {
                    string k = DataCheck.GetRegStr(e.Uri.ToString(),"&q=(.+?)&");
                    res.Add(k, ">>>>>>>>>>翻译失败  " + tran.error_code + "  " + tran.error_msg);
                }
            };

            foreach (var key in dic.Keys) {
                int salt = new Random((int)DateTime.Now.Ticks).Next();
                string u = url;
                u += "&salt=" + salt;// 随机数
                u += "&q=" + HttpUtility.UrlEncode(dic[key]);// 翻译内容，需转码
                u += "&sign=" + HttpUtility.UrlEncode(Encryption.EncryptMD5(appid + dic[key] + salt + appkey));// 签名
                crawler.Start(u).Wait();
            }

            return res;

        }

        /// <summary>
        /// 
        /// </summary>
        public class BaiduTransAPI {
            /// <summary>
            /// 翻译源语言
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// 译文语言
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// 翻译结果
            /// </summary>
            public List<TransRes> trans_result { get { if (t == null) t = new List<TransRes>(); return t; } set { t = value; } }
            private List<TransRes> t;

            /// <summary>
            /// 错误代码
            /// </summary>
            public string error_code { get; set; }
            /// <summary>
            /// 错误消息
            /// </summary>
            public string error_msg { get; set; }

            public class TransRes {
                /// <summary>
                /// 翻译源文字
                /// </summary>
                public string src { get; set; }
                /// <summary>
                /// 译文
                /// </summary>
                public string dst { get; set; }
            }

        }

    }
}