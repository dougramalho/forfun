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
        IHandleCommand<MarkDrinksServed>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<DrinksServed>
    {
        private bool open = false;
        private List<int> outstandingDrinks = new List<int>();

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

        public IEnumerable Handle(MarkDrinksServed c)
        {
            if (!AreDrinksOutstanding(c.MenuNumbers))
                throw new DrinksNotOutstanding();

            yield return new DrinksServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }
        
        public void Apply(TabOpened e)
        {
            open = true;
        }

        public void Apply(DrinksOrdered e)
        {
            outstandingDrinks.AddRange(e.Items.Select(i => i.MenuNumber));
        }

        public void Apply(DrinksServed e)
        {
            foreach (var num in e.MenuNumbers)
                outstandingDrinks.Remove(num);
        }

        private bool AreDrinksOutstanding(List<int> menuNumbers)
        {
            var curOutstanding = new List<int>(outstandingDrinks);
            foreach (var num in menuNumbers)
                if (curOutstanding.Contains(num))
                    curOutstanding.Remove(num);
                else
                    return false;
            return true;
        }
    }
}
