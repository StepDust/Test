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
    /// 字典值
    /// </summary>
    public class DataItemService : BaseDal<DataItem>
    {
        public DataItemService() : base(Config.Name_Demo) { }
    }
}
