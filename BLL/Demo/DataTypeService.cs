using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Demo
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public class DataTypeService : BaseService<Models.DataType>
    {
        public DataTypeService() : base(Config.Name_Demo) { }
    }
}
