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
namespace Apps.Models.Sys
{

	public partial class tbForecastModel:Virtual_tbForecastModel
	{
		
	}
	public class Virtual_tbForecastModel
	{
			public virtual string Id { get; set; }
			public virtual string Town { get; set; }
			public virtual string Forecast { get; set; }
			public virtual Nullable<double> Lon { get; set; }
			public virtual Nullable<double> Lat { get; set; }
			public virtual string Wind { get; set; }
			public virtual string Winf { get; set; }
			public virtual Nullable<System.DateTime> YBDatetime { get; set; }
			public virtual Nullable<int> Tmax { get; set; }
			public virtual Nullable<int> Tmin { get; set; }
		}
}
