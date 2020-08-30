﻿using EasyORM.Core;
using EasyORM.Service;
using System.Linq;
using NUnit.Framework;

namespace Test.Sql
{
    [TestFixture]
    public class Test_SqlCommandGenerator : SqlDBService<TestClass1>
    {
        public static string connStr = "Data Source=192.168.10.196;Initial Catalog=sqlStudy;User ID=sa;Password=sa123456;";
        public Test_SqlCommandGenerator() :base(connStr)
        {
        }

        [Test]
        public void Add()
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
            Assert.AreEqual(new[] { "@xingming", "@nianling", "@yuegongzi", "@bumenhao", "@dianhaua", "@bangongshi" }, SqlCommandGenerator.Context.Parameters.Keys.ToArray());
            Assert.AreEqual("INSERT INTO zhigong (xingming,nianling,yuegongzi,bumenhao,dianhaua,bangongshi) VALUES (@xingming,@nianling,@yuegongzi,@bumenhao,@dianhaua,@bangongshi)", SqlCommandGenerator.Context.SqlText);
        }

        [Test]
        public void Delete()
        {
        }

        [Test]
        public void Update()
        {
        }

        [Test]
        public void Query()
        {
            //var aa=base.Query();
        }
    }
}
