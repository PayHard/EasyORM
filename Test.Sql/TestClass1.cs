using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyORM.Core.EntityAttribute;

namespace Test.Sql
{
    [Table("zhigong")]
    public class TestClass1
    {
        [Key]
        [Auto]
        [Column]
        public int? zhigonghao { get; set; }

        [Column]
        public string xingming { get; set; }

        [Column]
        public int? nianling { get; set; }

        [Column]
        public decimal? yuegongzi { get; set; }

        [Column]
        public int? bumenhao { get; set; }

        [Column]
        public int? dianhaua { get; set; }

        [Column]
        public int? bangongshi { get; set; }
    }
}
