using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class BundleEntry
    {
        public UInt32 Id { get; set; }
        public UInt32 Address { get; set; }
        public Int32 Length { get; set; }

        public BundleEntry(UInt32 id, UInt32 address, Int32 length)
        {
            this.Id = id;
            this.Address = address;
            this.Length = length;
        }

        public override string ToString()
        {
            return String.Format("BundleEntry(id: {0} address: {1} length: {2})", this.Id, this.Address, this.Length);
        }
    }

}
