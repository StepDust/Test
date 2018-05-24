using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Common.Utils {
    /// <summary>
    /// Winform字体图标类
    /// Date：2018-05-23 15:54:11
    /// </summary>
    public static class WinFontIco {

        /**
         * 相关网址：https://fontawesome.com/icons
         * 相关帖子：https://www.cnblogs.com/isaboy/p/csharp_Font_Awesome_window_form_icon.html
         **/

        public static PrivateFontCollection CustomFont;

        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="control">Winform控件</param>
        /// <param name="brandIco">所选图标</param>
        /// <param name="fontSize">图标大小</param>
        /// <returns></returns>
        public static Control SetFontIco(this Control control, FontBrandIco brandIco, float fontSize = 9F) {
            control.Text = brandIco.ToString();
            return SetFont(control, "Brand", fontSize);
        }

        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="control">Winform控件</param>
        /// <param name="brandIco">所选图标</param>
        /// <param name="fontSize">图标大小</param>
        /// <returns></returns>
        public static Control SetFontIco(this Control control, FontFreeIco freeIco, float fontSize = 9F) {
            control.Text = freeIco.ToString();
            return SetFont(control, "Free", fontSize);
        }

        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="control">Winform控件</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">图标大小</param>
        /// <returns></returns>
        private static Control SetFont(Control control, string fontName, float fontSize) {

            AddFont();
             
            if (CustomFont.Families.Length > 0)
                control.Font = new Font(CustomFont.Families.Where(c => c.Name.Contains(fontName)).FirstOrDefault(), fontSize);

            return control;
        }

        /// <summary>
        /// 添加字体
        /// </summary>
        private static void AddFont() {
            if (CustomFont != null && Constant.IcoTtf.Length > 0) return;

            CustomFont = new PrivateFontCollection();
            string path = Path.GetFullPath(Constant.IcoTtf[0]);

            for (int i = 1; i < Constant.IcoTtf.Length; i++) {
                string ttfPath = path + "\\" + Constant.IcoTtf[i];
                if (File.Exists(ttfPath))
                    CustomFont.AddFontFile(ttfPath);// 字体的路径及名字 
            }
        }

    }

    /// <summary>
    /// 品牌图标
    /// </summary>
    public enum FontBrandIco {
        Android
    }

    /// <summary>
    /// 自由图标
    /// </summary>
    public enum FontFreeIco {
        Android,
        
    }

}
