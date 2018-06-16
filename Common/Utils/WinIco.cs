using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Common.Actions;

namespace Common.Utils {
    /// <summary>
    /// Winform字体类
    /// Date：2018-05-23 15:54:11
    /// </summary>
    public static class WinFont {

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
        public static Control SetFontIco(this Control control, string s, float fontSize = 9F) {
            control.Text = s;
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
            control.Text = StringUtils.UnicodeToString("\\u" + ((int)freeIco).ToString("X2"));
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
                string ttfPath = path + "\\" + Constant.IcoTtf[i] + ".ttf";
                if (File.Exists(ttfPath))
                    CustomFont.AddFontFile(ttfPath);// 字体的路径及名字 
            }
        }

        /// <summary>
        /// 返回本机已安装的所有字体
        /// </summary>
        /// <returns></returns>
        public static List<FontFamily> GetFontList() {
            return new InstalledFontCollection().Families.Where(c => !string.IsNullOrWhiteSpace(c.Name)).ToList();
        }


        public static List<font> GetIco() {
            List<font> fileList = new List<font>();

            string path = Path.GetFullPath(Constant.IcoTtf[0]);

            for (int i = 1; i < Constant.IcoTtf.Length; i++) {
                string ttfPath = path + "\\" + Constant.IcoTtf[i] + ".svg";
                if (File.Exists(ttfPath)) {
                    font t = SvgAction.SvgToObject<font>(ttfPath, "font");
                    if (fileList.Exists(c => c.font_face.font_family == t.font_face.font_family))
                        fileList.Find(c => c.id == t.id).glyph.AddRange(t.glyph);
                    else
                        fileList.Add(t);
                }
            }

            return fileList;
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
        Flag = 0xF024,
    }

    public class font : BaseSvg {

        public font() {
            glyph = new List<glyph>();
        }

        public string id { get; set; }
        public double horiz_adv_x { get; set; }

        public font_face font_face { get; set; }

        public List<glyph> glyph { get; set; }
    }

    public class font_face : BaseSvg {
        public string font_family { get; set; }

        public int units_per_em { get; set; }

        public int ascent { get; set; }

        public int descent { get; set; }

        public int font_weight { get; set; }

        public string font_style { get; set; }

    }

    public class glyph : BaseSvg {
        public string glyph_name { get; set; }
        public string unicode { get; set; }
        public string horiz_adv_x { get; set; }
        public string d { get; set; }

    }

}
