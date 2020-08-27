using System;
using EasyORM.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Sql
{
    [TestClass]
    public class Test_SqlCommandGenerator : SqlDBService<TestClass1>
    {
        public static string connStr = "Data Source=192.168.10.196;Initial Catalog=sqlStudy;User ID=sa;Password=sa123456;";
        public Test_SqlCommandGenerator() :base(connStr)
        {
        }

        [TestMethod]
        public void Add()
        {
            base.Add(new TestClass1()
            {
                zhigonghao = 100001,
                xingming="wangwu",
                nianling=22,
                yuegongzi=11111,
                bangongshi=1,
                bumenhao=1,
                dianhaua=111
            }) ;

        }

        [TestMethod]
        public void Delete()
        {
        }

        [TestMethod]
        public void Update()
        {
        }

        [TestMethod]
        public void Query()
        {
            //var aa=base.Query();
        }
    }
}
