using Common;
using Common.Actions;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WinForm {
    /// <summary>
    /// 系统颜色类
    /// Date：2018-05-29 09:13:40
    /// </summary>
    public class SystemColor {

        private static List<CustomColor> colorList;

        public List<CustomColor> GetColorList() {

            StringUtils.JsonToObject(FileAction.ReadToStr(Constant.CustomColor), colorList);

            if (colorList == null || colorList.Count <= 0) {
                CustomColor custom = new CustomColor()
                {
                    ColorName = "默认颜色",
                    IsUsing = true,
                    SystemBackGroundColor = Color.Black,
                    SystemForeColor = Color.White,
                    ControlBackGroundColor = Color.Blue,
                    ControlForeColor = Color.Blue
                };

                colorList = new List<CustomColor>
                {
                    custom
                };
            }

            return colorList;
        }

        public CustomColor GetUsingColor() {
            return GetColorList().Where(c => c.IsUsing).First();
        }

        public void SaveColor() {

        }
    }

    public class CustomColor {

        public string ColorName { get; set; }

        public bool IsUsing { get; set; }

        public Color SystemBackGroundColor { get; set; }

        public Color SystemForeColor { get; set; }

        public Color ControlBackGroundColor { get; set; }

        public Color ControlForeColor { get; set; }

        public Color BorderColor { get; set; }

    }
}
