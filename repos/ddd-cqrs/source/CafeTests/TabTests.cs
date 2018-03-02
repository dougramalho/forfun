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
        private OrderedItem testDrink1;
        private OrderedItem testDrink2;
        private OrderedItem testFood1;
        private OrderedItem testFood2;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 42;
            testWaiter = "Derek";

            testDrink1 = new OrderedItem
            {
                MenuNumber = 4,
                Description = "Sprite",
                Price = 1.50M,
                IsDrink = true
            };
            testDrink2 = new OrderedItem
            {
                MenuNumber = 10,
                Description = "Beer",
                Price = 2.50M,
                IsDrink = true
            };

            testFood1 = new OrderedItem
            {
                MenuNumber = 16,
                Description = "Beef Noodles",
                Price = 7.50M,
                IsDrink = false
            };
            testFood2 = new OrderedItem
            {
                MenuNumber = 25,
                Description = "Vegetable Curry",
                Price = 6.00M,
                IsDrink = false
            };
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

        [Test]
        public void CanNotOrderWithUnopenedTab()
        {
            Test(
                Given(),
                When(new PlaceOrder { 
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 }
                }), ThenFailWith<TabNotOpen>());
        }

        [Test]
        public void CanPlaceDrinksOrder()
        {
            Test(Given(new TabOpened
            {
                Id = testId,
                TableNumber = testTable,
                Waiter = testWaiter
            }),
            When(new PlaceOrder
            {
                Id = testId,
                Items = new List<OrderedItem> { testDrink1, testDrink2 }
            }),
            Then(new DrinksOrdered
            {
                Id = testId,
                Items = new List<OrderedItem> { testDrink1, testDrink2 }
            }));
        }

        [Test]
        public void CanPlaceFoodOrder()
        {
            Test(Given(new TabOpened
            {
                Id = testId,
                TableNumber = testTable,
                Waiter = testWaiter
            }),
            When(new PlaceOrder
            {
                Id = testId,
                Items = new List<OrderedItem> { testFood1, testFood2 }
            }),
            Then(new FoodOrdered
            {
                Id = testId,
                Items = new List<OrderedItem> { testFood1, testFood2 }
            }));
        }

        [Test]
        public void CanPlaceFoodAndDrinkOrder()
        {
            Test(Given(new TabOpened
            {
                Id = testId,
                TableNumber = testTable,
                Waiter = testWaiter
            }),
            When(new PlaceOrder
            {
                Id = testId,
                Items = new List<OrderedItem> { testFood1, testDrink2 }
            }),
            Then(new DrinksOrdered
            {
                Id = testId,
                Items = new List<OrderedItem> { testDrink2 }
            },
            new FoodOrdered
            {
                Id = testId,
                Items = new List<OrderedItem> { testFood1 }
            }));
        }
    }
}
