using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Iterator
{
    public class LeaderboardEntries : IEnumerable<string>
    {
        private List<string> data;
        private IterationOrder order;
        public enum IterationOrder
        {
            Basic,
            Reverse,
            Extra
        }
        public void SetIterationOrder(IterationOrder newOrder)
        {
            order = newOrder;
        }
        public LeaderboardEntries(List<string> data)
        {
            this.data = data;
        }

        public IEnumerator<string> GetEnumerator()
        {
            switch(order)
            {
                default:
                case IterationOrder.Basic:
                    return new Basic(this.data);
                case IterationOrder.Reverse:
                    return new Reverse(this.data);
                case IterationOrder.Extra:
                    return new Extra(this.data);
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
