using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace Common {
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileAction {

        /// <summary>
        /// 读取所有文字
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string ReadToStr(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            // 判断是否为相对路径
            if (!DataCheck.CheckReg(path[0] + "", DataCheck.Reg_Eng))
                HttpContext.Current.Server.MapPath(path);
            // 判断文件是否存在
            if (!File.Exists(path))
                return "";

            StreamReader read = new StreamReader(path, Encoding.Default);

            string str = File.ReadAllText(path, Encoding.Default);
            read.Close();
            read.Dispose();
            return str;
        }

        public static List<string> ReadToArr(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            // 判断是否为相对路径
            if (!DataCheck.CheckReg(path[0] + "", DataCheck.Reg_Eng))
                HttpContext.Current.Server.MapPath(path);
            // 判断文件是否存在
            if (!File.Exists(path))
                return null;

            StreamReader read = new StreamReader(path, Encoding.Default);
            List<string> res = new List<string>();

            while (!read.EndOfStream)
                res.Add(read.ReadLine());

            read.Close();
            read.Dispose();

            return res;
        }

        /// <summary>
        /// 读取两个字符之间的文字
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="startStr">起始字符</param>
        /// <param name="endStr">结束字符</param>
        /// <returns></returns>
        public static string ReadBetweenStr(string path, string startStr, string endStr)
        {
            List<string> text = ReadToArr(path);
            if (text != null)
                return "";

            string reg = string.Format(DataCheck.Reg_BetWeen, startStr, endStr);
            int starCon = 0;
            int endCon = 0;
            string res = "";
            foreach (var item in text) {
                string temp = DataCheck.GetRegStr(item, reg);
                // 单行匹配
                if (!string.IsNullOrEmpty(temp))
                    return temp;
                // 多行匹配
                if (DataCheck.CheckReg(item, startStr)) {
                    starCon++;
                }
                if (starCon > 0) {
                    res += item;
                }
                if (DataCheck.CheckReg(item, endStr))
                    endCon++;
                if (starCon == endCon)
                    return res;
            }

            return "";
        }

        /// <summary>
        /// 读取文件中预处理模块的文字
        /// </summary>
        /// <param name="notes">注释文字</param>
        /// <returns></returns>
        public static string ReadRegEdit(string path, string notes)
        {
            string startStr = "#region.*" + notes;
            string endStr = "#endregion";
            return ReadBetweenStr(path, startStr, endStr);
        }

        /// <summary>
        /// 更改文件
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="con">内容</param>
        public static void AppendStr(string path, string con)
        {
            // 判断是否为绝对路径
            if (!DataCheck.CheckReg(path[0] + "", DataCheck.Reg_Eng))
                return;
            int indexSuffix = path.LastIndexOf('.');
            int indexSlash = path.LastIndexOf('\\');

            if (indexSlash == -1 || indexSuffix == -1)
                return;
            if (indexSuffix <= indexSlash)
                return;

            string FileName = path.Substring(indexSlash);

            path = path.Substring(0, indexSlash);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            StreamWriter writer = new StreamWriter(path + FileName, true, Encoding.UTF8);
            writer.Write(con);
            writer.Flush();
            writer.Dispose();
        }

    }
}
