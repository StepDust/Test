using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace Common.Actions {
    /// <summary>
    /// 文件操作
    /// </summary>
    public static class FileAction {

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
            if (!Regular.CheckReg(path[0] + "", Regular.Reg_Eng))
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

        /// <summary>
        /// 读取文件为集合
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadToArr(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            // 判断是否为相对路径
            if (!Regular.CheckReg(path[0] + "", Regular.Reg_Eng))
                HttpContext.Current.Server.MapPath(path);
            // 判断文件是否存在
            if (!File.Exists(path))
                return null;

            StreamReader read = new StreamReader(path, Encoding.UTF8);
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

            string reg = string.Format(Regular.Reg_BetWeen, startStr, endStr);
            int starCon = 0;
            int endCon = 0;
            string res = "";
            foreach (var item in text) {
                string temp = Regular.GetRegStr(item, reg);
                // 单行匹配
                if (!string.IsNullOrEmpty(temp))
                    return temp;
                // 多行匹配
                if (Regular.CheckReg(item, startStr)) {
                    starCon++;
                }
                if (starCon > 0) {
                    res += item;
                }
                if (Regular.CheckReg(item, endStr))
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
            if (!Regular.CheckReg(path[0] + "", Regular.Reg_Eng))
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


        #region 读取文件夹下文件

        /// <summary>
        /// 读取目录下所有文件，包括子目录
        /// </summary>
        /// <param name="path">文件夹绝对路径</param>
        /// <param name="Suffix">文件后缀名</param>
        public static List<FileInfo> ReadDir(string path, string Suffix = "*")
        {
            List<FileInfo> list = new List<FileInfo>();

            if (Directory.Exists(path)) {
                // 获取当前目录
                DirectoryInfo Dir = new DirectoryInfo(path);
                list.AddRange(SetDirs(Dir, Suffix));
            }
            return list.Distinct().ToList();
        }

        /// <summary>
        /// 读取目录下所有文件
        /// </summary>
        /// <param name="directory"></param>
        public static List<FileInfo> SetDirs(DirectoryInfo directory, string Suffix = "*")
        {
            List<FileInfo> list = new List<FileInfo>();
            foreach (DirectoryInfo dir in directory.GetDirectories())
                // 访问子目录
                list.AddRange(SetDirs(dir, Suffix));
            // 寻找当前目录下文件
            list.AddRange(GetFiles(directory, Suffix));
            return list;
        }

        /// <summary>
        /// 读取文件夹下文件
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="Suffix">后缀名，默认为所有文件</param>
        public static List<FileInfo> GetFiles(DirectoryInfo directory, string Suffix = "*")
        {
            List<FileInfo> list = new List<FileInfo>();

            FileInfo[] files = directory.GetFiles();

            // 循环校验后缀名
            for (int i = 0; i < files.Length; i++) {
                if (files[i].Extension == "." + Suffix || Suffix == "*")
                    list.Add(files[i]);
            }
            return list;
        }

        #endregion

    }
}
