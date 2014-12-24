using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class BundleEntry
    {
        public uint id;
        public uint address;
        public int length;
        public override string ToString()
        {
            return String.Format("BundleEntry(id: {0} address: {1} length: {2})", id, address, length);
        }
    }

}
