using Common;
using Models;

namespace BLL.Demo {
    /// <summary>
    /// 字典表
    /// </summary>
    public class LangService : BaseService<Lang> {
        public LangService() : base(Config.Name_Demo) { }
    }
}
