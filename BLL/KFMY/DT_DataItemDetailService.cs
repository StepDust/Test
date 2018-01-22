using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.KFMY
{
    public class DT_DataItemDetailService : BaseDal<DT_DataItemDetail>
    {
        public DT_DataItemDetailService() : base(Config.Name_KFMY) { }
    }
}
