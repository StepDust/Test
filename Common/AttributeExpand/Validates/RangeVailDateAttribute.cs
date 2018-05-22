

namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 数值范围校验
    /// </summary>
    public class RangeVailDateAttribute : BaseValidateAttribute {
        private double oMin { get; set; }
        private double oMax { get; set; }

        /// <summary>
        /// 数值范围，[oMin,oMax]
        /// </summary>
        /// <param name="oMin">最小值</param>
        /// <param name="oMax">最大值</param>
        public RangeVailDateAttribute(double oMin, double oMax) {
            this.oMax = oMax;
            this.oMin = oMin;
            base.Remark = "值域：[{0},{1}]";
        }

        public override MsgRes Validate(object oVal) {

            MsgRes msg = new MsgRes(
              oVal == null || (double.Parse(oVal + "") >= this.oMin && double.Parse(oVal + "") <= this.oMax), string.Empty);

            if (msg.State == 0) msg.Msg = string.Format(base.Remark, oMin, oMax, oVal);

            return msg;

        }
    }
}
