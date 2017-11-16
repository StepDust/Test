using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CodeFirst {
    public class GSQ_WebService : BaseService<GSQ_Web> {
        public GSQ_WebService() : base(Config.Name_GSQ) { }
    }
}
