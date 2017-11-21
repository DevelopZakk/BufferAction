using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActionBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionBuffer.Tests
{
    [TestClass()]
    public class BufferActionTests
    {
        [TestMethod()]
        public void RecieveResultTest()
        {
            var act = new Act();
            var buffer = new BufferAction(1,1, act);

            buffer.RecieveResult(true);
            Assert.AreEqual(false,buffer.ActionStart);
            Assert.AreEqual(0,act.Counter);

            buffer.RecieveResult(true);
            Assert.AreEqual(false, buffer.ActionStart);
            Assert.AreEqual(0, act.Counter);

            buffer.RecieveResult(false);
            Assert.AreEqual(true,buffer.ActionStart);
            Assert.AreEqual(1,act.Counter);

            buffer.RecieveResult(false);
            Assert.AreEqual(1,act.Counter);


            var act2= new Act();
            var buf2 = new BufferAction(3,2,act2);

            var test = new TestProvider();
            foreach (var testSet in test.TestSet1())
            {
                buf2.RecieveResult(testSet.Test);
                Assert.AreEqual(act2.Counter,testSet.Counter);
                Assert.AreEqual(testSet.ActionStart,buf2.ActionStart);
            }
        }
    }

    public class TestProvider
    {
        public List<TestSet> TestSet1()
        {
            return new List<TestSet>()
            {
                new TestSet(false,0,false),
                new TestSet(false,0,false),
                new TestSet(true,0,false),
                new TestSet(true,0,false),
                new TestSet(false,0,false),
                new TestSet(false,0,false),
                new TestSet(false,0,false),
                new TestSet(true,0,false),
                new TestSet(false,0,false),
                new TestSet(true,0,false),
                new TestSet(false,0,false),
                new TestSet(false,0,false),
                new TestSet(false,1,true),
                new TestSet(false,1,true),
                new TestSet(false,1,true),
            };
        }
    }

    public class TestSet
    {
        public bool Test { get; }
        public bool ActionStart { get; }
        public int Counter { get; }

        public TestSet(bool test,int Counter, bool actionStart)
        {
            Test = test;
            ActionStart = actionStart;
            Counter = counter;
        }
    }

    public class Act : IAction
    {
        public int Counter { get; set; }

        public bool DoAction()
        {
            Counter += 1;
            return true;
        }
    }
}