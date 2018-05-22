using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces {
    public interface IMessage {
        int State { get; set; }
        string Msg { get; set; }
    }
}
