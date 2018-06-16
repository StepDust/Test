using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Common.Actions {
    /// <summary>
    /// SVG文件操作类
    /// Date：2018-05-28 15:59:42
    /// </summary>
    public static class SvgAction {

        public static T SvgToObject<T>(string filePath, string parentNode) where T : BaseSvg, new() {

            T model = new T
            {
                Layer = 0
            };
            svgList.Add(model);

            Type type = typeof(T);
            bool isBegin = false;

            if (!File.Exists(filePath) || type.Name != parentNode) return default;

            using (StreamReader read = new StreamReader(filePath)) {

                string line = "";
                int inex = 0;
                Regex reg;
                while (true) {
                    line += read.ReadLine();

                    inex++;
                    // 直到末尾结束
                    if (string.IsNullOrWhiteSpace(line)) break;

                    if (!isBegin) {
                        // 判断是否开始解析
                        reg = new Regex($"<\\s*{parentNode }\\s+");
                        isBegin = reg.IsMatch(line);
                        if (!isBegin) { line = ""; continue; }
                    }

                    reg = new Regex(@">");

                    // 包含结束字符
                    if (reg.IsMatch(line)) {
                        reg = new Regex(@"/[\w]*>");

                        CreateModel(line, svgList[0]?.GetType(), reg.IsMatch(line));
                        line = "";
                    }

                }
            }

            if (svgList.Count > 2)
                model = svgList[1] as T;
            else
                model = null;

            svgList = new List<BaseSvg>();

            return model;
        }

        public static List<BaseSvg> svgList = new List<BaseSvg>();

        private static void CreateModel(string line, Type type, bool isBr) {

            // 获取标签
            string[] lable = Regular.GetRegStrArr(line, @"<\s*(?<lable>[\w-]*)\s+", "lable");


            if (lable.Length == 1 && !string.IsNullOrWhiteSpace(lable[0])) {
                lable[0] = lable[0].Replace("-", "_");
                BaseSvg svg;
                if (isBr)
                    type = type.GetProperties().FirstOrDefault(c => c.Name == lable[0])?.PropertyType;

                if (type == null) return;

                // 判断指定class是否为泛型
                if (type.IsConstructedGenericType)
                    type = type.GenericTypeArguments.First();

                svg = Reflex.CreateModel<BaseSvg>(type);
                svg.Layer = svgList[0].Layer + 1;

                // 获取属性和值
                string[] par = Regular.GetRegStrArr(line, "\\s*[\\w-]+=\"[\\w-.&#x; ]+\"\\s*");

                // 设置属性和值
                foreach (var item in par) {
                    string[] key = item.Trim().Split('=');
                    if (key.Length != 2) continue;
                    key[0] = key[0].Replace("-", "_");

                    PropertyInfo property = type.GetProperty(key[0]);

                    if (property != null)
                        property.SetValue(svg, Convert.ChangeType(StringUtils.ReplaceStr(key[1], "\"", ""), property.PropertyType));
                }
                svgList.Add(svg);
                if (!isBr) svgList[0] = svg;
                else {
                    BaseSvg temp = svgList.FindLast(c => c.Equals(svgList[0]));

                    PropertyInfo pi = temp.GetType().GetProperty(type.Name);

                    if (pi != null) {
                        var pv = pi.GetValue(temp);
                        if (pi.PropertyType.IsConstructedGenericType) {
                            if (pi.PropertyType.GetInterface("IList`1") != null)
                                pv.GetType().GetMethod("Add").Invoke(pv, parameters: new object[] { svg });
                        }
                        else
                            pv = svg;
                        pi.SetValue(temp, pv);
                    }
                }

            }

        }

    }

    public abstract class BaseSvg {
        public int Layer { get; set; }
    }

}
