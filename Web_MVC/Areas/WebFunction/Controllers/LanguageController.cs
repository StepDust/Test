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

        #region 翻译

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

            string reg = "msgid \"(?<msgid>.*)\"\r\nmsgstr \"(?<msgstr>.*)\"";

            string[] strArr = DataCheck.GetRegStrArr(content, reg);

            foreach (var str in strArr) {
                MatchCollection res = Regex.Matches(str, reg, RegexOptions.IgnoreCase);
                foreach (Match item in res) {
                    dic.Add(item.Groups["msgid"].Value, item.Groups["msgstr"].Value);
                }
            }

            SaveLanJs(dic);
            // 发送翻译请求
            // Dictionary<string, string> dicRes= BaiduTranslate(dic, Lan, "");

            return Content(ResObj.LayerMsg("转换成功！", Icon.Success, "Index"));
        }

        public void SaveLanJs(Dictionary<string, string> dic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var lang = '';\n");
            builder.Append("/**\n");
            builder.Append("* 获取指定名称的cookie的值\n");
            builder.Append("* @param {string} objName\n");
            builder.Append("*/\n");
            builder.Append("function getCookie(objName) {\n");
            builder.Append("    var arrStr = document.cookie.split('; ');\n");
            builder.Append("    for (var i = 0; i < arrStr.length; i++) {\n");
            builder.Append("        var temp = arrStr[i].split('=');\n");
            builder.Append("        if (temp[0] == objName) return unescape(temp[1]);\n");
            builder.Append("    }\n");
            builder.Append("    return '';\n");
            builder.Append("}\n");
            builder.Append("\n\n");
            builder.Append("var CN = {\n");
            builder.Append("    GetLang: function (name) {\n");
            builder.Append("        // 判断中文英文\n");
            builder.Append("        if (lang == '')\n");
            builder.Append("            lang = getCookie('culture');\n");
            builder.Append("        if (lang != 'zh-CN' || lang != 'en')\n");
            builder.Append("            lang = 'zh-CN';\n");
            builder.Append("\n");
            builder.Append("        if (lang == 'zh-CN')\n");
            builder.Append("            return name;\n");
            builder.Append("        var str = '';\n");
            builder.Append("        switch (name) { \n");

            foreach (var item in dic.Keys) {

                string key = DataCheck.RepLanguage(item).Replace("'", "\\\'");
                string val = DataCheck.RepLanguage(dic[item]).Replace("'", "\\\'");

                builder.Append($"            case '{key}': str = '{val}'; break;\n");
            }
            builder.Append($"            default : str = name; break;\n");

            builder.Append("        }\n");
            builder.Append("        return str;\n");
            builder.Append("    }\n");
            builder.Append("}\n");

            FileAction.AppendStr("F:\\Lang.js", builder.ToString());

        }

        #endregion

        #region js中文替换



        #endregion


    }
}