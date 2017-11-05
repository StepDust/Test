using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public class GTKey {

        #region 文件名称，File

        /// <summary>
        /// 导航栏数据文件名称
        /// </summary>
        public static string File_Nav => "Nav.data";

        #endregion

        #region 消息名称，Msg

        #region 增删改

        /// <summary>
        /// 添加失败
        /// </summary>
        public static string Msg_Error_Add => "添加失败！";
        /// <summary>
        /// 添加成功
        /// </summary>
        public static string Msg_Succes_Add => "添加成功！";

        /// <summary>
        /// 删除失败
        /// </summary>
        public static string Msg_Error_Del => "删除失败！";
        /// <summary>
        /// 删除成功
        /// </summary>
        public static string Msg_Succes_Del => "删除成功！";

        /// <summary>
        /// 修改失败
        /// </summary>
        public static string Msg_Error_Edit => "修改失败！";
        /// <summary>
        /// 修改成功
        /// </summary>
        public static string Msg_Succes_Edit => "修改成功！";
        #endregion

        #endregion

    }
}
