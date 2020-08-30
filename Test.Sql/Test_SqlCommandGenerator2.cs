using EasyORM.Core;
using EasyORM.Service;
using System.Linq;
using Xunit;

namespace Test.Sql
{
    public class Test_SqlCommandGenerator2 : SqlDBService<TestClass1>
    {
        public static string connStr = "Data Source=192.168.10.196;Initial Catalog=sqlStudy;User ID=sa;Password=sa123456;";
        public Test_SqlCommandGenerator2() :base(connStr)
        {
        }
        
        [Fact]
        public void Add_Test()
        {
            base.Add(new TestClass1()
            {
                zhigonghao = 100001,
                xingming="wangwu",
                nianling=22,
                yuegongzi=11111,
                bumenhao=1,
                dianhaua= 111,
                bangongshi = 1
            }) ;
            //var aa = new[] { "@xingming", "@nianling", "@yuegongzi", "@bumenhao", "@dianhaua", "@bangongshi" };
            //var bb = SqlCommandGenerator.Context.Parameters.Keys.ToArray();
            //Assert.AreEqual(aa, bb);
            Assert.Equal(new []{ "@xingming", "@nianling", "@yuegongzi", "@bumenhao", "@dianhaua","@bangongshi" }, SqlCommandGenerator.Context.Parameters.Keys.ToArray());
            Assert.Equal("INSERT INTO zhigong (xingming,nianling,yuegongzi,bumenhao,dianhaua,bangongshi) VALUES (@xingming,@nianling,@yuegongzi,@bumenhao,@dianhaua,@bangongshi)", SqlCommandGenerator.Context.SqlText);
        }
        [Fact]
        public void Delete_Test()
        {
        }
        [Fact]
        public void Update_Test()
        {
        }
        [Fact]
        public void Query_Test()
        {
            //var aa=base.Query();
        }
    }
}
