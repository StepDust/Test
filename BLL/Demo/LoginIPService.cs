using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Common;

namespace BLL.Demo
{
    /// <summary>
    /// 登录IP获取
    /// </summary>
    public class LoginIPService : BaseService<LoginIP>
    {
        public LoginIPService() : base(Config.Name_Demo) { }
    }
}
