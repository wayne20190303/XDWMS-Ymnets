//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WMS_Part
    {
        public WMS_Part()
        {
            this.WMS_PO = new HashSet<WMS_PO>();
            this.WMS_ReturnOrder = new HashSet<WMS_ReturnOrder>();
            this.WMS_AI = new HashSet<WMS_AI>();
            this.WMS_Product_Entry = new HashSet<WMS_Product_Entry>();
        }
    
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string PartType { get; set; }
        public string CustomerCode { get; set; }
        public string LogisticsCode { get; set; }
        public string OtherCode { get; set; }
        public Nullable<decimal> PCS { get; set; }
        public string StoreMan { get; set; }
        public string Status { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string ModifyPerson { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
    
        public virtual ICollection<WMS_PO> WMS_PO { get; set; }
        public virtual ICollection<WMS_ReturnOrder> WMS_ReturnOrder { get; set; }
    }
}
