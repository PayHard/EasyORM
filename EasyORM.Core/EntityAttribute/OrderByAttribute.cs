using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.EntityAttribute
{
    /// <summary>
    /// 查询的数据指定默认的排序属性，若方法内指定属性则以指定排序属性为准
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OrderByAttribute : Attribute
    {

    }
}
