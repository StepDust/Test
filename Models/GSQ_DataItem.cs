//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GSQ_DataItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> IsTree { get; set; }
        public Nullable<int> SortCode { get; set; }
        public string Description { get; set; }
        public int EnabledMark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
