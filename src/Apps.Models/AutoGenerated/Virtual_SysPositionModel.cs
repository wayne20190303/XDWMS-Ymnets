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
namespace Apps.Models.Sys
{

	public partial class SysPositionModel:Virtual_SysPositionModel
	{
		
	}
	public class Virtual_SysPositionModel
	{
		[Display(Name = "GUID主键")]
		public virtual string Id { get; set; }
		[Display(Name = "职位名称")]
		public virtual string Name { get; set; }
		[Display(Name = "职位说明")]
		public virtual string Remark { get; set; }
		[Display(Name = "排序")]
		public virtual int Sort { get; set; }
		[Display(Name = "创建时间")]
		public virtual System.DateTime CreateTime { get; set; }
		[Display(Name = "状态")]
		public virtual bool Enable { get; set; }
		[Display(Name = "职位允许人数")]
		public virtual int MemberCount { get; set; }
		[Display(Name = "所属部门")]
		public virtual string DepId { get; set; }
		}
}
