using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tab
{
    public class OrderedItem
    {
        public int MenuNumber;
        public string Description;
        public bool IsDrink;
        public decimal Price;
    }

    public class DrinksOrdered
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }

    public class FoodOrdered
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }
}
