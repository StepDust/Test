using Common.Encrypt;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Web;

namespace Common {
    /// <summary>
    /// 翻译类
    /// </summary>
    public class Translate {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">AppID</param>
        /// <param name="key">密钥</param>
        public Translate(string id, string key) {
            appid = id;
            appkey = key;
        }

        /// <summary>
        /// AppID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string appkey { get; set; }

        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="dic">字典数据</param>
        /// <param name="Lan">翻译类型</param>
        /// <param name="path">保存路径</param>
        public Dictionary<string, string> BaiduTranslate(Dictionary<string, string> dic, string Lan, string path) {
            Dictionary<string, string> res = new Dictionary<string, string>();

            foreach (var key in dic.Keys) {
                string[] lanArr = Lan.Split(';');
                BaiduTransAPI tran = BaiduTranslate(dic[key], lanArr[0], lanArr[1]);

                if (tran.trans_result.Count > 0)
                    res.Add(tran.trans_result[0].src, tran.trans_result[0].dst);
                else {
                    res.Add(dic[key], ">>>>>>>>>>翻译失败  " + tran.error_code + "  " + tran.error_msg);
                }
            }

            return res;
        }

        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="str">待翻译文字</param>
        /// <param name="form"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public BaiduTransAPI BaiduTranslate(string str, string form, string to) {
            string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";

            // 拼接翻译类型参数
            url += "from=" + form;// 翻译源语言
            url += "&to=" + to;//  译文语言
            url += "&appid=" + appid;// 接口ID
            int salt = new Random((int)DateTime.Now.Ticks).Next();
            string u = url;
            u += "&salt=" + salt;// 随机数
            u += "&q=" + HttpUtility.UrlEncode(str);// 翻译内容，需转码
            u += "&sign=" + HttpUtility.UrlEncode(MD5Encrypt.Encrypt(appid + str + salt + appkey));// 签名
            BaiduTransAPI tran = GetReqObj<BaiduTransAPI>(u);
            return tran;
        }


        public T GetReqObj<T>(string url)
            where T : class, new() {
            T obj = new T();
            obj = EnythingUtils.JsonToObject(EnythingUtils.GetUrlHtml(url), obj) as T;

            return obj;
        }

    }



    /// <summary>
    /// 百度翻译结果对象
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
