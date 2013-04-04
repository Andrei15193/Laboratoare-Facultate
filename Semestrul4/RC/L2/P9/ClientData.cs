using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCLab2
{
    class ClientData
    {
        public ClientData()
        {
        }

        public ClientData(short value)
        {
            this.Add(value);
        }

        public void Add(short value)
        {
            if (this.currentList < 2)
                if (value == 0)
                    this.currentList++;
                else
                    this.clientInput[this.currentList].AddLast(value);
        }

        public byte CurrentCount
        {
            get
            {
                return this.currentList;
            }
        }

        public IEnumerable<short> Extract()
        {
            return this.clientInput[0].Except(this.clientInput[1]);
        }

        public bool NeedsToRead
        {
            get
            {
                return this.currentList != 2;
            }
        }

        private byte currentList = 0;
        private LinkedList<short>[] clientInput = { new LinkedList<short>(), new LinkedList<short>() };
    }
}
