
namespace Common.AttributeExpand.Validates {
    /// <summary>
    /// 非空校验
    /// </summary>
    public class RequirdVailDateAttribute : BaseValidateAttribute {

        public RequirdVailDateAttribute() {
            base.Remark = "是必须的";
        }

        public override MsgRes Validate(object oVal) {
            MsgRes result = new MsgRes(oVal != null, string.Empty);

            if (result.State == 0) result.Msg = base.Remark;

            return result;
        }
    }
}
