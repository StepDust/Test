using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Demo
{
    /// <summary>
    /// 聊天内容
    /// </summary>
    public class S_ChatMsgService : BaseService<S_ChatMsg>
    {
        public S_ChatMsgService() : base(Config.Name_Demo) { }
    }
}
