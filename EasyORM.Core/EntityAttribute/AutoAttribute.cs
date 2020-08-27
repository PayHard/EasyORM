using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.EntityAttribute
{
    /// <summary>
    /// 表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AutoAttribute : Attribute
    {
    }
}
