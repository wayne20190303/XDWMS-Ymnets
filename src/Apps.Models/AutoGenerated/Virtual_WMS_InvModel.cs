//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Apps.Models;
using System;
using System.ComponentModel.DataAnnotations;
namespace Apps.Models.WMS
{

	public partial class WMS_InvModel:Virtual_WMS_InvModel
	{
		
	}
	public class Virtual_WMS_InvModel
	{
		[Display(Name = "未设置")]
		public virtual int Id { get; set; }
		[Display(Name = "未设置")]
		public virtual int InvId { get; set; }
		[Display(Name = "未设置")]
		public virtual Nullable<int> SubInvId { get; set; }
		[Display(Name = "未设置")]
		public virtual int PartId { get; set; }
		[Display(Name = "未设置")]
		public virtual string Lot { get; set; }
		[Display(Name = "现有量")]
		public virtual decimal Qty { get; set; }
		[Display(Name = "当前出库数")]
		public virtual Nullable<decimal> OutQty { get; set; }
		[Display(Name = "备料数")]
		public virtual Nullable<decimal> StockQty { get; set; }
		}
}
