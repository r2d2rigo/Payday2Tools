using System;
using System.Collections.Generic;
using System.IO;

namespace BundleLib
{
    public class BundleHeader
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
                using (BinaryReader headerReader = new BinaryReader(bundleHeaderStream))
                {
                    headerReader.ReadUInt32();
                    headerReader.ReadUInt32();

                    UInt32 bundleEntryCount = headerReader.ReadUInt32();

                    headerReader.ReadUInt32();
                    
                    bool hasEntryLength = headerReader.ReadUInt32() == 24;

                    if (hasEntryLength)
                    {
                        bundleHeaderStream.Position += 2 * 4;
                    }

                    for (int i = 0; i < bundleEntryCount; i++)
                    {
                        UInt32 id = headerReader.ReadUInt32();
                        UInt32 address = headerReader.ReadUInt32();
                        Int32 length = -1;

                        if (hasEntryLength)
                        {
                            length = headerReader.ReadInt32();
                        }
                        else
                        {
                            if (i > 0)
                            {
                                BundleEntry previousEntry = loadedHeader.Entries[i - 1];
                                previousEntry.Length = (Int32)(address - previousEntry.Address);
                            }
                        }

                        BundleEntry newEntry = new BundleEntry(id, address, length);

                        loadedHeader.Entries.Add(newEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading bundle header: " + ex.Message);

                return null;
            }

            return loadedHeader;
        }
    }
}
