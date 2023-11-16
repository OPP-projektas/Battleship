using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Iterator
{
    public class Extra : IEnumerator<string>
    {
        private List<string> data;
        private int index = -1;

        public Extra(List<string> data)
        {
            this.data = data;
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
            index+=2;
            return (index < data.Count);
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}
