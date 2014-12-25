using System;

namespace BundleLib
{
    public class BundleEntry
    {
        public UInt32 Id { get; private set; }
        public UInt32 Address { get; private set; }
        // TODO: fix BundleHeader.Load and make the setter private
        public Int32 Length { get; set; }

        public BundleEntry(UInt32 id, UInt32 address, Int32 length)
        {
            this.Id = id;
            this.Address = address;
            this.Length = length;
        }
    }
}
