using Cafe.Tab;
using Edument.CQRS;
using Events.Tab;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeTests
{
    [TestFixture]
    public class TabTests : BDDTest<TabAggregate>
    {
        private Guid testId;
        private int testTable;
        private string testWaiter;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 42;
            testWaiter = "Derek";
        }

        [Test]
        public void CanOpenANewTab()
        {
            Test(
                Given(),
                When(new OpenTab
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                Then(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }));
        }
    }
}
