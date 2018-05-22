using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.AttributeExpand {

    public interface IBaseValidateAttribute {
        string Remark { get; set; }
        IMessage Validate(object oVal);
    }

}