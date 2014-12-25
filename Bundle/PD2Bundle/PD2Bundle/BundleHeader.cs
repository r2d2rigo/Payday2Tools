using System;
using System.Collections.Generic;
using System.IO;

namespace PD2Bundle
{
    class BundleHeader
    {
        public List<BundleEntry> Entries { get; private set; }

        public BundleHeader()
        {
            this.Entries = new List<BundleEntry>();
        }

        public static BundleHeader Load(Stream bundleHeaderStream)
        {
            BundleHeader loadedHeader = new BundleHeader();

            try
            {
                uint item_count;
                bool has_length;

                using (BinaryReader br = new BinaryReader(bundleHeaderStream))
                {
                    br.ReadUInt32();
                    br.ReadUInt32();
                    item_count = br.ReadUInt32();
                    br.ReadUInt32();
                    has_length = br.ReadUInt32() == 24;
                    if (has_length)
                    {
                        bundleHeaderStream.Position += 2 * 4;
                    }
                    for (int i = 0; i < item_count; ++i)
                    {
                        UInt32 id = br.ReadUInt32();
                        UInt32 address = br.ReadUInt32();
                        Int32 length = 0;

                        if (has_length)
                        {
                            length = br.ReadInt32();
                        }

                        BundleEntry be = new BundleEntry(id, address, length);

                        loadedHeader.Entries.Add(be);
                        if (!has_length && i > 0)
                        {
                            BundleEntry pbe = loadedHeader.Entries[i - 1];
                            pbe.Length = (int)be.Address - (int)pbe.Address;
                        }
                    }
                }
                if (item_count > 0 && !has_length)
                {
                    loadedHeader.Entries[loadedHeader.Entries.Count - 1].Length = -1;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return loadedHeader;
        }
    }
}
