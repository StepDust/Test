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
    /// 即时聊天用户
    /// </summary>
    public class S_UserService : BaseService<S_User>
    {
        public S_UserService() : base(Config.Name_Demo) { }
    }
}
