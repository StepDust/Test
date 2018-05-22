using System;

namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 时间校验
    /// Date：2018-05-21 19:29:05
    /// </summary>
    public class DateTimeValidateAttribute : BaseValidateAttribute {

        private DateTime? dTime { get; set; }

        public DateTimeValidateAttribute(DateTime? dTime) {

            this.dTime = dTime;

            base.Remark = "时间不能小于{0}";
        }

        public DateTimeValidateAttribute() {

            this.dTime = new DateTime(1900, 1, 1);

            base.Remark = "时间不能小于{0}";
        }

        public override MsgRes Validate(object oVal) {
            MsgRes result = new MsgRes(
                oVal == null || dTime == null || (dTime <= (DateTime)oVal)
                , string.Empty);

            if (result.State == 0) result.Msg = string.Format(base.Remark, this.dTime);

            return result;
        }
    }
}