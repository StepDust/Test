﻿using Common.Actions;
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
        public static string[] DataBaseContext => ConfigAction.GetAppSetting("DataBaseContext").Split(';');

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static string RedisBaseContext => ConfigAction.GetAppSetting("RedisBaseContext");

        /// <summary>
        /// Redis前缀
        /// </summary>
        public static string CustomKey => ConfigAction.GetAppSetting("CustomKey");
        #endregion

        /// <summary>
        /// WinForm窗体字体图标，[字体文件路径，ttf1，ttf2，ttf3...]
        /// </summary>
        public static string[] IcoTtf => ConfigAction.GetAppSetting("IcoTtf").Split(';');

        /// <summary>
        /// WinForm窗体字体图标，[字体文件路径，ttf1，ttf2，ttf3...]
        /// </summary>
        public static string CustomColor => ConfigAction.GetAppSetting("CustomColor");
    }
}
