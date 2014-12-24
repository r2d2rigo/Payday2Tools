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
                            BundleEntry be = new BundleEntry();
                            be.id = br.ReadUInt32();
                            be.address = br.ReadUInt32();
                            if (has_length)
                            {
                                be.length = br.ReadInt32();
                            }

                            loadedHeader.Entries.Add(be);
                            if (!has_length && i > 0)
                            {
                                BundleEntry pbe = loadedHeader.Entries[i - 1];
                                pbe.length = (int)be.address - (int)pbe.address;
                            }
                        }
                    }
                }
                if (item_count > 0 && !has_length)
                {
                    loadedHeader.Entries[loadedHeader.Entries.Count - 1].length = -1;
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
