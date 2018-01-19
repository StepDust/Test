using System.Configuration;
using System.IO;
using System.Web;

namespace Common {
    /// <summary>
    /// 配置文件操作
    /// </summary>
    public class Config : ConfigurationSection {

        #region 配置信息

        /// <summary>
        /// Demo数据库
        /// </summary>
        public const string Name_Demo = "DemoEntities";

        /// <summary>
        /// CodeFirst数据库
        /// </summary>
        public const string Name_CodeFirst = "CodeFirst";

        /// <summary>
        /// GSQ_PaChong数据库
        /// </summary>
        public const string Name_GSQ = "GSQ_PaChongEntities";
        
        #endregion

        /// <summary>
        /// 返回配置文件对象
        /// </summary>
        /// <param name="Porject">指定项目</param>
        /// <returns></returns>
        private static Configuration GetConfig(string Porject) {

            // 获取当前项目根路径
            string path = HttpContext.Current.Server.MapPath("~");

            // 若要跳转至其他项目路径
            if (!string.IsNullOrEmpty(Porject))
                path = Utils.DelLastChar(path, "\\", 2) + "\\" + Porject + "\\";

            // 判断是否存在项目路径
            if (!Directory.Exists(path))
                return null;
            // 判断当前路径是否包含App.config或Web.config
            if (File.Exists(path + "App.config"))
                path += "App.config";
            else if (File.Exists(path + "Web.config"))
                path += "Web.config";
            else
                return null;

            // 创建配置文件映射
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = path;
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        #region 读取信息

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <param name="name">连接字符串名称</param>
        /// <param name="Porject">项目名称，默认：Models</param>
        public static string ReadConStr(string name, string Porject = "Models") {
            Configuration config = GetConfig(Porject);
            if (config != null)
                return config.ConnectionStrings.ConnectionStrings[name]?.ConnectionString;
            return "";
        }

        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="name">连接字符串名称</param>
        /// <param name="Porject">项目名称，默认：当前项目</param>
        public static string ReadAppSet(string key, string Porject = "") {
            Configuration config = GetConfig(Porject);
            if (config != null)
                return config.AppSettings.Settings[key]?.Value;
            return "";
        }

        #endregion





    }
}
