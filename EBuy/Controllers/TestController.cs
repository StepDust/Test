using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EBuy.Controllers {
    /// <summary>
    /// 
    /// </summary>
    public class TestController : Controller {
        
        public ActionResult Index() {
            return View();
        }

        List<FileInfo> list;
        List<string> Set = new List<string>();
        public static data data;

        [HttpPost]
        public ActionResult Index(string name, string path, string type, string save) {
            data = new data();
            data.path = path;
            data.save = save;
            data.name = name;
            data.type = type;

            ViewBag.path = data.path;
            ViewBag.save = data.save;
            ViewBag.type = data.type;
            ViewBag.name = data.name;

            // 获取所有文件
            ReadDir(data.path);

            Set = new List<string>();

            // 读取文件信息
            foreach (var item in list)
                ReadFile(item);


            ViewBag.files = list;
            // 拼接文件信息
            string txt = "";
            foreach (var item in Set.ToList()) {
                string t = item.Split('~')[0];
                txt += "" +
                    "msgid \"" + t.Substring(0, t.Length - 1) + "\" \n" +
                    "msgstr \"" + t.Substring(0, t.Length - 1) + "\" \n\n";
            }
            // 导出信息
            SaveFile(txt, data.save, data.name);

            return View();
        }

        [HttpPost]
        public ActionResult Join(string path) {
            ViewBag.join = path;
            Set = new List<string>();
            data = new data();
            data.path = path;
            data.type = "po";

            ReadDir(path);

            foreach (var item in list)
                JoinInfo(item);


            // 拼接文件信息
            string txt = "";
            foreach (var item in Set.ToList()) {
                string t = item;
                txt += "" +
                    "msgid \"" + t + "\" \n" +
                    "msgstr \"" + t + "\" \n\n";
            }
            // 导出信息
            SaveFile(txt, path, "All.po");

            return Redirect("Index");
        }

        #region 文件操作

        /// <summary>
        /// 读取文件夹
        /// </summary>
        /// <param name="path"></param>
        public void ReadDir(string path) {
            list = new List<FileInfo>();

            if (Directory.Exists(path)) {
                // 获取文件夹
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                foreach (DirectoryInfo dir in TheFolder.GetDirectories())
                    SetDirs(dir);
                SetFiles(TheFolder);
            }
        }

        /// <summary>
        /// 遍历文件夹
        /// </summary>
        /// <param name="directory"></param>
        public void SetDirs(DirectoryInfo directory) {
            foreach (DirectoryInfo dir in directory.GetDirectories())
                SetDirs(dir);
            SetFiles(directory);
        }

        /// <summary>
        /// 读取文件夹下文件
        /// </summary>
        /// <param name="directory"></param>
        public void SetFiles(DirectoryInfo directory) {
            FileInfo[] files = directory.GetFiles();

            for (int i = 0; i < files.Length; i++) {
                if (files[i].Extension == "." + data.type)
                    list.Add(files[i]);
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public void SaveFile(string txt, string path, string name) {
            txt += "共 " + Set.Count + " 条！";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            StreamWriter writer = new StreamWriter(path + "\\" + name);
            writer.Write(txt);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        #endregion

        #region 字符处理
        
        /// <summary>
        /// 读取文件信息，并扣出字符串
        /// </summary>
        /// <param name="file"></param>
        public void ReadFile(FileInfo file) {
            StreamReader read = new StreamReader(file.FullName);
            string str = read.ReadToEnd();
            read.Close();
            read.Dispose();


            MatchCollection mc = Check(str);

            string txt = "";
            for (int i = 0; i < mc.Count; i++) {
                txt += mc[i].Value + "\n";

                int j = 0;
                string t = mc[i].Value.Trim();
                //t = GetKH(t);
                for (; j < Set.Count; j++) {
                    if (Set[j].Split('~').Contains(t + "\t"))
                        break;
                }

                if (j == Set.Count) {

                    if (!string.IsNullOrEmpty(t))
                        Set.Add(t + "\t~\t" + file.Name);
                }

            }
        }

        /// <summary>
        /// 整合信息
        /// </summary>
        /// <param name="file"></param>
        public void JoinInfo(FileInfo file) {
            StreamReader read = new StreamReader(file.FullName);
            string str = read.ReadToEnd();
            read.Close();
            read.Dispose();

            string temp = "";

            //"msgid \".{ 0, }\""
            Regex reg = new Regex("msgid.{0,}");
            MatchCollection mc = reg.Matches(str);
            foreach (Match m in mc) {
                temp = m.Value.Replace("msgid", "").Replace("\"", "").Trim();
               // MatchCollection c = Check(temp);
                //foreach (Match item in c) {
                //    temp = item.Value;
                    if (!Set.Contains(temp) && !string.IsNullOrEmpty(temp))
                        Set.Add(temp);
               // }
            }
        }

        /// <summary>
        /// 返回匹配数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public MatchCollection Check(string str) {
            // 去掉注释
            str = Remove(str, "@*", "*@");
            str = Remove(str, "<!--", "-->");
            str = Remove(str, "/*", "*/");

            Regex reg = null;

            // 去掉双斜杠注释
            reg = new Regex("[/]+[/]+.{0,}");
            str = reg.Replace(str, "");
            // 去掉预编译注释 #region
            reg = new Regex("[#]{1}[a-z]{1,}.{0,}");
            str = reg.Replace(str, "");

            // 寻找中文
            reg = new Regex("" +
                "[\u4e00-\u9fa5]{1,}" +
                "[\\s，,‘“;(（）)：、:.&\\-a-zA-Z0-9\u4e00-\u9fa5]{0,}" +
                "[。”’！0-9\u4e00-\u9fa5]{1,}");
            return reg.Matches(str);
        }

        /// <summary>
        /// 去注释
        /// </summary>
        /// <param name="str"></param>
        /// <param name="starStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public string Remove(string str, string starStr, string endStr) {
            int staIindex = str.IndexOf(starStr);
            int endIndex = str.IndexOf(endStr);

            int max = starStr.Length > endStr.Length ? starStr.Length : endStr.Length;

            while (staIindex >= 0 && endIndex >= 0) {
                if (endIndex - staIindex < max) {
                    str = str.Remove(endIndex, endStr.Length);
                    endIndex = str.IndexOf(endStr);
                }

                str = str.Substring(0, staIindex) + str.Substring(endIndex + endStr.Length);
                staIindex = str.IndexOf(starStr);
                endIndex = str.IndexOf(endStr);
            }
            return str;
        }
        
        /// <summary>
        /// 补全括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetKH(string str) {

            char Z = '*';
            char F = '*';

            if (str.Contains("("))
                Z = '(';
            else if (str.Contains("（"))
                Z = '（';

            if (str.Contains(")"))
                F = ')';
            else if (str.Contains("）"))
                Z = '）';

            if (F == '*' && Z == '*')
                return str;

            if (F != '*' && Z != '*')
                return str;

            if (F != '*') {
                if (F == ')')
                    return '(' + str;
                return '（' + str;
            }
            if (Z != '*') {
                if (Z == '(')
                    return str + ')';
                return str + '）';
            }
            return str;
        }

        #endregion
    }

    public class data {
        public string save;
        public string path;
        public string type;
        public string name;
    }
}