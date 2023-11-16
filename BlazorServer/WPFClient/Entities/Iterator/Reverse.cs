using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Iterator
{
    public class Reverse : IEnumerator<string>
    {
        private List<string> data;
        private int index = -1;

        public Reverse(List<string> data)
        {
            this.data = data;
            this.index = data.Count;
        }

        public string Current
        {
            get { return data[index]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            index--;
            return (index >= 0);
        }

        public void Reset()
        {
            index = data.Count;
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}
