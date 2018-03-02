using Edument.CQRS;
using Events.Tab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Tab
{
    public class TabAggregate : Aggregate, 
        IHandleCommand<OpenTab>,
        IHandleCommand<PlaceOrder>,
        IApplyEvent<TabOpened>
    {
        private bool open = false;

        public IEnumerable Handle(OpenTab c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }

        public IEnumerable Handle(PlaceOrder c)
        {
            if (!open)
                throw new TabNotOpen();

            var drink = c.Items.Where(x => x.IsDrink).ToList();

            if (drink.Any())
                yield return new DrinksOrdered
                {
                    Id = c.Id,
                    Items = drink
                };

            var food = c.Items.Where(i => !i.IsDrink).ToList();
            if (food.Any())
                yield return new FoodOrdered
                {
                    Id = c.Id,
                    Items = food
                };
        }

        public void Apply(TabOpened e)
        {
            open = true;
        }
    }
}
