using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DDS
{
    public class DT_DataItemService : BaseService<DT_DataItem>
    {
        public DT_DataItemService() : base(Config.Name_KFMY) { }
    }
}
