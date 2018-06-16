using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Common.Actions {
    /// <summary>
    /// Xml操作类
    /// </summary>
    public static class XmlAction {

        /// <summary>
        /// 字符串序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T StrToXml<T>(string content) where T : new() {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content))) {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                return (T)xmlFormat.Deserialize(stream);
            }
        }

        /// <summary>
        /// 文件反序列化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(string filePath) where T : new() {
            using (Stream fStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite)) {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                return (T)xmlFormat.Deserialize(fStream);
            }
        }
    }
}
