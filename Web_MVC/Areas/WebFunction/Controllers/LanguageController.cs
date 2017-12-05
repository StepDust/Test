using Common;
using Microsoft.International.Converters.PinYinConverter;
using NPinyin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers
{
    public class LanguageController : Manager
    {
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

            foreach (var str in strArr)
            {
                MatchCollection res = Regex.Matches(str, reg, RegexOptions.IgnoreCase);
                foreach (Match item in res)
                {
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

            foreach (var item in dic.Keys)
            {

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

        public ActionResult RepFile(string path_js, string path_po, string path_out)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            #region 文件数据读取

            List<FileInfo> JSFileList = FileAction.ReadDir(path_js, "js");

            if (JSFileList == null || JSFileList.Count <= 0)
                return Error("不存在此目录，或目录下无对应文件！", "Index", false);

            #endregion

            // 赋予必要参数
            RepFilePara.dic = GetLanPo(path_po);
            RepFilePara.path = path_js;
            RepFilePara.outpath = path_out;
            RepFilePara.errInfo = new List<string>();
            Thread thread;

            // 循环所有文件
            foreach (var item in JSFileList)
            {
                RepFilePara para = new RepFilePara
                {
                    file = item
                };
                // 每一个文件，开辟一个线程
                thread = new Thread(new ThreadStart(para.RepFile));
                thread.Start();
            }

            // 输出日志信息，保存为文件
            StringBuilder builder = new StringBuilder();
            foreach (var item in RepFilePara.errInfo)
            {
                builder.Append(item + "\n");
            }
            FileAction.AppendStr(path_out + "\\log.txt", builder.ToString());

            watch.Stop();
            return Success($"导出成功！开启线程{JSFileList.Count}个，" +
                $"用时：{watch.ElapsedMilliseconds * 1.0 / 1000 / 60}分钟", "Index", false);
        }

        /// <summary>
        /// 文件替换类
        /// </summary>
        public class RepFilePara
        {
            /// <summary>
            /// 待匹配文件
            /// </summary>
            public FileInfo file;
            /// <summary>
            /// 读取路径
            /// </summary>
            public static string path;
            /// <summary>
            /// 输出路径，配合读取路径，可以将原目录的结构复制出来
            /// </summary>
            public static string outpath;
            /// <summary>
            /// 语言包字典
            /// </summary>
            public static Dictionary<string, string> dic;
            /// <summary>
            /// 所有替换信息
            /// </summary>
            public static List<string> errInfo = new List<string>();

            public void RepFile()
            {
                try
                {
                    List<string> list = FileAction.ReadToArr(file.FullName);
                    string p = file.FullName.Replace(path, outpath);

                    // 匹配注释正则
                    string regNotes = "([/]{2,}|[*]+).*";
                    // 匹配中文正则
                    string regChinese = @"([\u4e00-\u9fa5]{1,}[\s，,‘“;(（）)：、:.&\\-a-zA-Z0-9\u4e00-\u9fa5]{0,}[。”’！0-9\u4e00-\u9fa5]{1,})|([\u4e00-\u9fa5]{1})";
                    int index = 0;

                    foreach (var item in list)
                    {
                        index++;
                        // 去掉注释
                        string str = DataCheck.RepStr(item.Trim(), regNotes, "");
                        //是否包含中文
                        if (!DataCheck.CheckReg(str, regChinese))
                            FileAction.AppendStr(p, item + "\n");
                        else
                        {

                            // 取出中文
                            string[] strArr = DataCheck.GetRegStrArr(str, regChinese);
                            string temp = str;
                            string get = "";
                            // 在语言包中寻找匹配
                            foreach (var chinese in strArr)
                            {

                                // 如果没有包含汉字，查找下一个
                                if (!DataCheck.CheckReg(chinese, "[\u4e00-\u9fa5]+"))
                                    continue;

                                // 若语言包中存在对应中文，直接替换
                                if (dic.ContainsKey(chinese))
                                {

                                    get = dic[chinese];
                                    temp = temp.Replace(chinese, DataCheck.RepLanguage(dic[chinese], false));
                                    errInfo.Add($"{chinese}\t{dic[chinese]}\t{file.FullName}");
                                }
                                // 否则，去寻找最类似的中文
                                else
                                {
                                    // 获取极限长度
                                    int min = chinese.Length - 2;
                                    int max = chinese.Length + 2;
                                    // 判断是否替换
                                    bool bl = false;
                                    // 循环字典
                                    foreach (var key in dic.Keys)
                                    {
                                        // 超出极限长度，则跳出
                                        if (max < key.Length || key.Length < min)
                                            continue;
                                        // 若符合极限长度，且包含当前文字
                                        if (key.Contains(chinese))
                                        {
                                            // 进行替换
                                            temp = temp.Replace(chinese, DataCheck.RepLanguage(dic[key], false));
                                            errInfo.Insert(0, $"^{chinese}：{index}行\t{dic[key]}\t{file.FullName}");
                                            bl = true;
                                        }
                                    }
                                    if (!bl)
                                        errInfo.Insert(0, $"^^{chinese}：{index}行\t{file.FullName}");
                                }
                            }

                            // 将当前行写入文件
                            FileAction.AppendStr(p, item.Replace(str, temp) + "\n");
                        }
                    }

                }
                catch (Exception e)
                {
                    errInfo.Add("错误：" + file.FullName + "\t" + e.Message);
                }
            }
        }

        #endregion

        #region 语言包排版

        public ActionResult Layout(string path_po)
        {
            Dictionary<string, string> dic = GetLanPo(path_po);
            List<Po> poList = new List<Po>();

            foreach (var key in dic.Keys)
            {
                poList.Add(new Po(key, dic[key]));
            }

            // 获取最大长度
            int max = poList.Select(c => c.msgid).Max(c => c.Length);
            List<Po> res = new List<Po>();

            // 依次取出
            for (int i = 1; i <= max; i++)
            {
                List<Po> temp = poList.Where(c => c.msgid.Length == i).OrderBy(c => c.msgid).ToList();
                if (temp != null && temp.Count > 0)
                    res.AddRange(temp);
            }



            foreach (var item in res)
            {
                // 单词长度小于三个的时候，首字母大写
                FileAction.AppendStr(path_po + "_out", Line(item.msgid, item.msgstr.Trim()));
            }
            FileAction.AppendStr(path_po + "_out", $"共{res.Count}条！");

            return Success("排版完成！", "Index", false);
        }


        public string Line(string msgid, string msgstr)
        {
            Dictionary<string, string> area = new Dictionary<string, string>();
            area.Add("州", "Prefecture");
            area.Add("省", "Province");
            area.Add("市", "City");
            area.Add("县", "County");
            area.Add("区", "District");
            area.Add("镇", "Town");
            area.Add("乡", "Country");

            string last = msgid.Substring(msgid.Length - 1);


            // 若为城市名称
            if (area.Keys.Contains(last))
            {
                msgstr = "";
                foreach (var item in msgid.Substring(0, msgid.Length - 1))
                {
                    // 使用NPingYin插件
                    string t = Utils.StrToUpper(Pinyin.GetPinyin(item)).Trim();

                    if (Regex.IsMatch(t, "[\u4e00-\u9fa5]"))
                    {
                        // 使用微软自带转换插件
                        ChineseChar p = new ChineseChar(item);
                        t = p.Pinyins.FirstOrDefault();
                        if (t == null)
                            t = item + "";
                        else
                            t = t.Substring(0, t.Length - 1);
                        t = Utils.StrToUpper(t);
                    }

                    msgstr += t + "'";
                }
                msgstr = Utils.DelLastChar(msgstr, "'");
                msgstr += " " + area[last];
            }

            // 去掉多重空格
            Regex regex = new Regex(" +");
            msgstr = regex.Replace(msgstr, " ");

            return $"msgid \"{msgid}\"\nmsgstr \"{Utils.StrToUpper(msgstr.Trim(), 2)}\"\n\n";
        }

        public class Po
        {
            public Po(string msgid, string msgstr)
            {
                this.msgid = msgid;
                this.msgstr = msgstr;
            }
            public string msgid;
            public string msgstr;
        }

        #endregion

        /// <summary>
        /// 返回语言包数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetLanPo(string path)
        {
            string PoFileCon = FileAction.ReadToStr(path);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string reg = "msgid \"(?<msgid>.*)\"\nmsgstr \"(?<msgstr>.*)\"";

            string[] strArr = DataCheck.GetRegStrArr(PoFileCon, reg);

            foreach (var str in strArr)
            {
                MatchCollection res = Regex.Matches(str, reg, RegexOptions.IgnoreCase);
                foreach (Match item in res)
                {
                    if (dic.Keys.Contains(item.Groups["msgid"].Value))
                        continue;
                    dic.Add(item.Groups["msgid"].Value, item.Groups["msgstr"].Value);
                }
            }
            return dic;
        }

    }
}