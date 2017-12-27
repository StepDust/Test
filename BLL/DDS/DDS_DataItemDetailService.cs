using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DDS
{
   public class DDS_DataItemDetailService : BaseService<DDS_DataItemDetail>
    {
        public DDS_DataItemDetailService() : base(Config.Name_DDS) { }
    }
}
