using Common.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constant {

        #region 数据库配置，必须

        /// <summary>
        /// 数据库对象，[Model层绝对路径，数据库类全名]
        /// </summary>
        public static string[] DataBaseContext { get => ConfigAction.GetAppSetting("DataBaseContext").Split(';'); }

        /// <summary>
        /// Dal层生成的dll文件绝对路径
        /// </summary>
        public static string DalPath => ConfigAction.GetAppSetting("DalPath");

        /// <summary>
        /// Bll层生成的dll文件绝对路径
        /// </summary>
        public static string BllPath => ConfigAction.GetAppSetting("BllPath");

        /// <summary>
        /// 基础数据操作类全名，实现接口IBaseDal
        /// </summary>
        public static string IBaseDal => ConfigAction.GetAppSetting("IBaseDal");

        /// <summary>
        /// Dal命名空间
        /// </summary>
        public static string BaseDal => ConfigAction.GetAppSetting("BaseDal");


        public static string BaseService => ConfigAction.GetAppSetting("BaseService");

        #endregion


    }
}
