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

        public static BundleHeader Load(string bundle_id)
        {
            BundleHeader loadedHeader = new BundleHeader();
            string header = bundle_id + "_h.bundle";

            if(!File.Exists(header))
            {
                Console.WriteLine("Bundle header file does not exist.");
                return null;
            }
            try
            {
                uint item_count;
                bool has_length;
                using (FileStream fs = new FileStream(header, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        br.ReadUInt32();
                        br.ReadUInt32();
                        item_count = br.ReadUInt32();
                        br.ReadUInt32();
                        has_length = br.ReadUInt32() == 24;
                        if (has_length)
                        {
                            fs.Position += 2 * 4;
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
