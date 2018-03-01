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
    public class TabAggregate : Aggregate, IHandleCommand<OpenTab>
    {

        public IEnumerable Handle(OpenTab c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }
    }
}
