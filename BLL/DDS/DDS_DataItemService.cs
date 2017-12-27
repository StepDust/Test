using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DDS
{
    public class DDS_DataItemService : BaseService<DDS_DataItem>
    {
        public DDS_DataItemService() : base(Config.Name_DDS) { }
    }
}
