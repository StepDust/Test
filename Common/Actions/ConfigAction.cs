using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 配置文件操作
    /// </summary>
    public static class ConfigAction {
        /// <summary>
        /// 配置文件字典缓存
        /// </summary>
        private static Dictionary<string, string> AppSettings = new Dictionary<string, string>();

        /// <summary>
        /// 添加配置字典
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static string AddAppSetting(string key, Func<string> action) {
            string res = string.Empty;

            if (!AppSettings.Keys.Contains(key)) {
                res = action.Invoke();
                if (res != null)
                    AppSettings.Add(key, res);
            }
            else res = AppSettings[key];

            return res;
        }

        /// <summary>
        /// 读取配置文件appSettings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">配置名称</param>
        /// <returns>配置值</returns>
        public static string GetAppSetting(string key) {
            return AddAppSetting(key, () => ConfigurationManager.AppSettings[key]);
        }

        /// <summary>
        /// 返回连接字符串
        /// </summary>
        /// <param name="index">连接字符串索引</param>
        /// <returns></returns>
        public static string GetConStr(int index = 0) {
            return AddAppSetting($"ConnectionStrings[{index}]", () => ConfigurationManager.ConnectionStrings[index].ConnectionString);
        }

        /// <summary>
        /// 返回连接字符串
        /// </summary>
        /// <param name="name">连接字符串名称</param>
        /// <returns></returns>
        public static string GetConStr(string name) {
            return AddAppSetting($"ConnectionStrings[{name}]", () => ConfigurationManager.ConnectionStrings[name].ConnectionString);
        }

    }
}
