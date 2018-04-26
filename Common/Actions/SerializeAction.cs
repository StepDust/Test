using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common {
    /// <summary>
    /// 序列化操作
    /// </summary>
    public class SerializeAction {

        /// <summary>
        /// 返回物理路径
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        private static string PhysicsPath(string FileName, string Path = "") {
            Path = EnythingUtils.PathHandle(Path);
            if (!string.IsNullOrEmpty(Path))
                Path += "\\";
            Path = "~\\App_Data\\" + Path;
            Path = HttpContext.Current.Server.MapPath(Path);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            return Path + FileName;
        }

        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="Obj">待序列化对象</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="Path">序列化路径，相对于App_Data文件夹</param>
        public static void ToSerialize<T>(T Obj, string FileName, string Path = "") {
            Path = PhysicsPath(FileName, Path);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(Path, FileMode.Create);
            bf.Serialize(fileStream, Obj);

            fileStream.Close();
            fileStream.Dispose();
        }
    
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileName">文件名称</param>
        /// <param name="Path">序列化路径，相对于App_Data文件夹</param>
        /// <returns></returns>
        public static T DeSerializeNow<T>(string FileName, string Path = "")
            where T : class, new() {
            Path = PhysicsPath(FileName, Path);
            if (!File.Exists(Path))
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(Path, FileMode.Open);
            T obj = bf.Deserialize(fileStream) as T;
            fileStream.Close();
            fileStream.Dispose();

            return obj;
        }

    }
}
