
namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 长度校验
    /// </summary>
    public class LengthVailDateAttribute : BaseValidateAttribute {

        private int iMin { get; set; }
        private int iMax { get; set; }

        /// <summary>
        /// 长度验证，[iMin,iMax]
        /// </summary>
        /// <param name="iMin">最短长度</param>
        /// <param name="iMax">最长长度</param>
        public LengthVailDateAttribute(int iMin, int iMax) {
            this.iMin = iMin;

            this.iMax = iMax;

            if (iMin == iMax) base.Remark = "长度必须等于{0}";// 固定长度
            else if (iMin <= 0 && 0 < iMax) base.Remark = "长度必须不大于{1}";// 最大长度
            else if (iMin > 0 && 0 >= iMax) base.Remark = "长度必须不小于{0}";// 最小长度
            else base.Remark = "长度范围：[{0},{1}]";// 长度范围
        }

        public override MsgRes Validate(object oVal) {
            MsgRes result = new MsgRes(
                oVal == null ||
               ((iMin == iMax && oVal.ToString().Length == iMin) ||// 固定长度
                (iMin <= 0 && 0 < iMax && oVal.ToString().Length <= iMax) ||// 最大长度
                (iMin > 0 && 0 >= iMax && oVal.ToString().Length >= iMin) ||// 最小长度
                (iMin <= oVal.ToString().Length && oVal.ToString().Length <= iMax))// 长度范围
                , string.Empty);

            if (result.State == 0) result.Msg = string.Format(base.Remark, iMin, iMax, oVal);

            return result;
        }
    }
}
